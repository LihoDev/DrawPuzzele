using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementStarter : MonoBehaviour
{
    [SerializeField] private List<CharacterMover> _characters = new();
    [SerializeField] private UnityEvent OnStartMoving;
    [SerializeField] private UnityEvent OnEndMoving;

    public void StartMoving()
    {
        if (IsCanStart())
        {
            foreach (CharacterMover character in _characters)
                character.StartMoving();
            OnStartMoving?.Invoke();
        }
    }

    public void StopMoving()
    {
        foreach (CharacterMover character in _characters)
            character.StopMoving();
        OnEndMoving?.Invoke();
    }

    private bool IsCanStart()
    {
        foreach(CharacterMover character in _characters)
        {
            if (!character.IsHasRoute())
                return false;
        }
        return true;
    }
}
