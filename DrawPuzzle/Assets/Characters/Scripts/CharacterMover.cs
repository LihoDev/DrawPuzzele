using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private StartDrawingPoint _startPoint;
    [SerializeField] private float _minDistanceToPoint = 0.1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
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
        Vector3 previousPointPosition = transform.position;
        Vector3 previousFramePosition = transform.position;
        Vector3[] points = _route.GetPoints();
        bool moveRight = false;
        float elapsedTime = 0f;
        float segmentDuration = _movementDuration / points.Length;
        foreach (var point in points)
        {
            while ((point - transform.position).sqrMagnitude > _minDistanceToPoint)
            {
                transform.position = Vector3.Lerp(previousPointPosition, point, elapsedTime / segmentDuration);
                if (moveRight != transform.position.x > previousFramePosition.x)
                    _spriteRenderer.flipX = !_spriteRenderer.flipX;
                moveRight = transform.position.x > previousFramePosition.x;
                previousFramePosition = transform.position;
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
    
            previousPointPosition = transform.position;
            elapsedTime = 0;
        }
        _animator.SetTrigger("Idle");
        OnEndRoute?.Invoke();
    }
}
