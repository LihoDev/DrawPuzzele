using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new skins", menuName = "Skins/Create new skins", order = 1)]
public class CharacterSkins : ScriptableObject
{
    [SerializeField] private List<AnimatorOverrideController> _animators = new List<AnimatorOverrideController>();
    [SerializeField] private List<Transform> _previewPrefab = new List<Transform>();

    public AnimatorOverrideController GetAnimatorController(int index)
    {
        return _animators[index];
    }

    public Transform GetPreviewPrefab(int index)
    {
        return _previewPrefab[index];
    }

    public int GetAnimatorsCount()
    {
        return _animators.Count;
    }
}
