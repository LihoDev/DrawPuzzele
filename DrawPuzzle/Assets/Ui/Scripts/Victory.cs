using UnityEngine;

public class Victory : EndGameMenu
{
    [SerializeField, Min(0)] private int _countVictoryPoints = 2;
    private int _countPoints = 0;

    public void AddPoint()
    {
        _countPoints++;
        if (_countPoints == _countVictoryPoints)
            ShowMenu();
    }
}
