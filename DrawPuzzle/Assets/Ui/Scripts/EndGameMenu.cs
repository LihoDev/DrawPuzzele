using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _visual;

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowMenu()
    {
        _visual.SetActive(true);
    }
}
