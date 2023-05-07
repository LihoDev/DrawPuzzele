using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new skins", menuName = "Skins/Create new skins", order = 1)]
public class CharacterSkins : ScriptableObject
{
    [SerializeField] private List<AnimatorOverrideController> _animators = new List<AnimatorOverrideController>();
    [SerializeField] private List<Sprite> _preview = new List<Sprite>();
    private int _selected = 0;

    public int Selected { 
        set
        {
            if (value > _animators.Count - 1)
                Debug.LogWarning("Selected is out of range");
            else
                _selected = value;
        }
        get => _selected;
    }

    public AnimatorOverrideController AnimatorController { get => _animators[_selected]; }

    public Sprite GetPreview(int index)
    {
        return _preview[index];
    }

    //public AnimatorOverrideController GetAnimatorController()
    //{
    //    return _animators[Selected];
    //}

    //public Transform GetPreviewPrefab(int index)
    //{
    //    return _previewPrefab[index];
    //}

    public int GetAnimatorsCount()
    {
        return _animators.Count;
    }
}
