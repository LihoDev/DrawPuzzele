using System.Collections.Generic;
using UnityEngine;

public class SkinSwitch : MonoBehaviour
{
    [SerializeField] private List<CharacterSkins> _skins = new List<CharacterSkins>();
    [SerializeField] private Transform _scrollViewContent;

    private List<int> _selected = new List<int>();

    private static SkinSwitch _instance;

    public static int GetSelectedIndex(CharacterSkins skins)
    {
        int i = _instance.GetSkinsIndex(skins);
        if (i == -1)
            return _instance._selected[i];
        return 0;
    }

    public static void ChooseSkin(CharacterSkins skins, int index)
    {
        for (var i = 0; i < _instance._skins.Count && _instance._skins[i] != skins; i++)
            if (_instance._skins[i] == skins)
                _instance._selected[i] = index;
    }

    private int GetSkinsIndex(CharacterSkins skins)
    {
        for (var i = 0; i < _instance._skins.Count; i++)
            if (_instance._skins[i] = skins)
                return i;
        Debug.LogWarning("Skins is not found");
        return -1;
    }

    private void Awake()
    {
        if (FindObjectsByType<SkinSwitch>(FindObjectsSortMode.None).Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _instance = this;
        for (var i = 0; i< _skins.Count; i++)
            _selected.Add(0);
    }

    private void Start()
    {
        foreach (CharacterSkins skin in _skins)
            for (var i = 0; i < skin.GetAnimatorsCount(); i++)
                Instantiate(skin.GetPreviewPrefab(i), Vector3.zero, Quaternion.identity, _scrollViewContent);
    }
}
