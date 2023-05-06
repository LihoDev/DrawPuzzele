using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private string _nextScene;

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
}
