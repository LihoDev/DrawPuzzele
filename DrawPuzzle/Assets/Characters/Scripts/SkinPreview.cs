using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinPreview : MonoBehaviour
{
    [SerializeField] private CharacterSkins _skins;
    [SerializeField] private int index;
    
    public void ChooseSkin()
    {
        SkinSwitch.ChooseSkin(_skins, index);
    }
}
