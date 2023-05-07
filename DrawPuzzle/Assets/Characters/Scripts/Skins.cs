using System.Collections.Generic;
using UnityEngine;

public class Skins<T> : SkinsPreview
{
    [SerializeField] private List<T> _skins = new List<T>();

    public T Sprite { get => _skins[_selected]; }
}
