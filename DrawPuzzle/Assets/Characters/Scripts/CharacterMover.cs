using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _movementDuration = 3f;
    [SerializeField] private StartDrawingPoint _startPoint;
    [SerializeField] private float _minDistanceToPoint = 0.1f;
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
        _movement = StartCoroutine(MoveCharacter());
    }

    public void StopMoving()
    {
        StopCoroutine(_movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
            OnCollision?.Invoke();
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
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            previousPosition = transform.position;
            elapsedTime = 0;
        }
        OnEndRoute?.Invoke();
    }
}
