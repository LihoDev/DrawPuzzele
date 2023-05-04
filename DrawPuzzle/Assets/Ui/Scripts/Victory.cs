using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : EndGameMenu
{
    [SerializeField] private string _nextScene;
    [SerializeField, Min(0)] private int _countVictoryPoints = 2;
    private int _countPoints = 0;

    public void AddPoint()
    {
        _countPoints++;
        if (_countPoints == _countVictoryPoints)
            ShowMenu();
    }

    private void StartNextLevel()
    {
        if (string.IsNullOrEmpty(_nextScene))
            return;
        SceneManager.LoadScene(_nextScene);
    }
}
