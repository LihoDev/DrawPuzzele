using System.Collections.Generic;
using UnityEngine;

public class SkinsPreview : ScriptableObject
{
    [SerializeField] private List<Sprite> _preview = new List<Sprite>();
    protected int _selected = 0;

    public int Selected
    {
        set
        {
            if (value > _preview.Count - 1)
                Debug.LogWarning("Selected is out of range");
            else
                _selected = value;
        }
        get => _selected;
    }

    public Sprite GetPreview(int index)
    {
        return _preview[index];
    }

    public int GetPreviewCount()
    {
        return _preview.Count;
    }
}
