using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Skin : MonoBehaviour
{
    [SerializeField] private CharacterSkins _skins;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();  
    }

    private void Start()
    {
        _animator.runtimeAnimatorController = _skins.GetAnimatorController(SkinSwitch.GetSelectedIndex(_skins));
    }
}
