using UnityEngine;

public class EndDrawingPoint : MonoBehaviour
{
    [SerializeField] private Party _party = Party.Red;
    public Party Party { get => _party;}
}
