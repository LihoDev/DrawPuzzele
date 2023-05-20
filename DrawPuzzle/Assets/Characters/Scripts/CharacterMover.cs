using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private StartDrawingPoint _startPoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _invertFlipX;
    [SerializeField] private UnityEvent OnCollision;
    [SerializeField] private UnityEvent OnEndRoute;
    private Coroutine _movement;
    private Line _route;
    
    public bool IsHasRoute()
    {
        _route = _startPoint.GetLine();
        return _route != null;
    }

    public void StartMoving()
    {
        if (_route == null)
            return;
        _animator.SetTrigger("Run");
        _movement = StartCoroutine(MoveCharacter());
    }

    public void StopMoving()
    {
        StopCoroutine(_movement);
        _animator.SetTrigger("Idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterMover character)) 
        {
            _animator.SetTrigger("Fight");
            OnCollision?.Invoke();
        }
        else if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _animator.SetTrigger("Death");
            OnCollision?.Invoke();
        }       
    }

    private IEnumerator MoveCharacter()
    {
        Vector3[] points = _route.GetPoints();
        Vector3 previousFramePosition = transform.position;
        float speed = _route.GetLength() / _movementDuration;
        int indexPoint = 1;
        transform.position = points[0];
        while (indexPoint + 1 < points.Length)
        {
            if ((points[indexPoint] - transform.position).normalized == Vector3.zero && indexPoint + 1 < points.Length)
                indexPoint++;
            Vector3 newPosition = transform.position + (points[indexPoint] - transform.position).normalized * speed * Time.deltaTime;
            float pointDistance = 0; 
            float newPositionDistance = Vector3.Distance(transform.position, newPosition);
            while (Vector3.Distance(transform.position, newPosition) > Vector3.Distance(transform.position, points[indexPoint]) && indexPoint + 1 < points.Length)
            {
                pointDistance += Vector3.Distance(transform.position, points[indexPoint]);
                transform.position = points[indexPoint];
                indexPoint++;
            }
            transform.position = transform.position + (points[indexPoint] - transform.position).normalized * (newPositionDistance - pointDistance); 
            _spriteRenderer.flipX = _invertFlipX ^ transform.position.x > previousFramePosition.x;
            previousFramePosition = transform.position;
            yield return new WaitForEndOfFrame();
        }
        _animator.SetTrigger("Idle");
        OnEndRoute?.Invoke();
    }
}
