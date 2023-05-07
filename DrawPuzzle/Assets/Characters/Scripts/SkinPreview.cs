using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkinPreview : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color32 _notSelected;
    private CharacterSkins _character;
    private int _selected;
    private UnityAction OnChooseSkin;

    public void SetSkin(CharacterSkins skin, int index, UnityAction onChooseSkin)
    {
        _character = skin;
        _selected = index;
        _image.sprite = skin.GetPreview(index);
        DisplaySelection();
        OnChooseSkin = onChooseSkin;
    }

    public void DisplaySelection()
    {
        _image.color = (_character.Selected != _selected)? _notSelected: new Color32(255, 255, 255, 255);
    }

    public void ChooseSkin()
    {
        _character.Selected = _selected;
        OnChooseSkin?.Invoke();
        //SkinSwitch.ChooseSkin(_skins, _index);
    }
}
