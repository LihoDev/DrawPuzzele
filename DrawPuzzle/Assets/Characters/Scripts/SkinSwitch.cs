using System.Collections.Generic;
using UnityEngine;

public class SkinSwitch : MonoBehaviour
{
    [SerializeField] private List<SkinsPreview> _characters = new List<SkinsPreview>();
    [SerializeField] private Transform _scrollViewContent;
    [SerializeField] private SkinPreview _skinPreviewPrefab;
    private List<SkinPreview> _previews = new List<SkinPreview>();

    public void RefreshDisplaySelection()
    {
        foreach (var preview in _previews)
            preview.DisplaySelection();
    }

    private void Start()
    {
        InstantiatePrefabs();
    }

    private void InstantiatePrefabs()
    {
        foreach (var skin in _characters)
            for (var i = 0; i < skin.GetPreviewCount(); i++)
            {
                SkinPreview preview = Instantiate(_skinPreviewPrefab, Vector3.zero, Quaternion.identity, _scrollViewContent).GetComponent<SkinPreview>();
                preview.SetSkin(skin, i, delegate { RefreshDisplaySelection(); });
                _previews.Add(preview);
            }
    }
}
