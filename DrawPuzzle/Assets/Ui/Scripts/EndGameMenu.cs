using UnityEngine;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _visual;

    public void ShowMenu()
    {
        _visual.SetActive(true);
    }
}
