using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    [SerializeField] private string _mainMenu;

    public void StartLevel()
    {
        if (string.IsNullOrEmpty(_nextScene))
            return;
        SceneManager.LoadScene(_nextScene);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        if (string.IsNullOrEmpty(_mainMenu))
            return;
        SceneManager.LoadScene(_mainMenu);
    }
}
