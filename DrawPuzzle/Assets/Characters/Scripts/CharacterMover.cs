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
        Vector3 previousFramePosition = transform.position;
        Vector3[] points = _route.GetPoints();
        float fullDistance = _route.GetLength();
        float distanceNextPoint = 0;
        float lastPointTime;
        float nextPointTime;
        float elapsedTime = 0;
        for (var i = 0; i < points.Length - 1; i++)
        {
            distanceNextPoint += Vector3.Distance(points[i], points[i + 1]);
            lastPointTime = elapsedTime;
            nextPointTime = (_movementDuration * distanceNextPoint) / fullDistance;
            while ((points[i + 1] - transform.position).sqrMagnitude > _minDistanceToPoint)
            {
                elapsedTime+= Time.deltaTime;
                transform.position = Vector3.Lerp(points[i], points[i + 1], elapsedTime / (nextPointTime - lastPointTime));
                distanceNextPoint += Vector3.Distance(transform.position, previousFramePosition);
                _spriteRenderer.flipX = _invertFlipX ^ transform.position.x > previousFramePosition.x;
                previousFramePosition = transform.position;
                yield return new WaitForEndOfFrame();
            }
        }
        _animator.SetTrigger("Idle");
        OnEndRoute?.Invoke();
    }
}
