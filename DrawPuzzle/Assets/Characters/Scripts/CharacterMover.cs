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
    [SerializeField] private bool _defaultFlipX = false;
    [SerializeField] private UnityEvent OnCollision;
    [SerializeField] private UnityEvent OnEndRoute;
    //private bool _flipX;
    //private bool FlipX 
    //{ 
    //    set
    //    {
    //        _flipX = _defaultFlipX ^ value;
    //    }
    //}
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

    private void Start()
    {
        _spriteRenderer.flipX = _defaultFlipX;
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
        Vector3 previousPosition = transform.position;
        Vector3[] points = _route.GetPoints();
        float elapsedTime = 0;
        float segmentDuration = _movementDuration / points.Length;
        foreach (var point in points)
        {
            while ((point - transform.position).sqrMagnitude > _minDistanceToPoint)
            {
                transform.position = Vector3.Lerp(previousPosition, point, elapsedTime / segmentDuration);
                _spriteRenderer.flipX = _defaultFlipX ^ (transform.position - previousPosition).x < 0;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            previousPosition = transform.position;
            elapsedTime = 0;
        }
        _animator.SetTrigger("Idle");
        OnEndRoute?.Invoke();
    }
}
