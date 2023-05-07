using UnityEngine;

public class StartDrawingPoint : MonoBehaviour
{
    [SerializeField] private Line _prefab;
    private Line _instencedLine;

    public Line GetLine()
    {
        return _instencedLine;
    }

    public Line InstantiateLine(Vector3 position)
    {
        _instencedLine = Instantiate(_prefab, position, Quaternion.identity);
        return _instencedLine;   
    }
}
