using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    [SerializeField, Min(0)] private float _segmentFrequency = 0.1f;
    [SerializeField] private Party _party = Party.Blue;
    public Party Party { get => _party; }

    public void DrawSegment(Vector2 position)
    {
        if (!IsCanIncreased(position))
            return;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, position);
    }

    public Vector3[] GetPoints()
    {
        Vector3[] points = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(points);
        return points;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private bool IsCanIncreased(Vector2 position)
    {
        if (_lineRenderer.positionCount == 0)
            return true;
        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), position) > _segmentFrequency;
    }
}
