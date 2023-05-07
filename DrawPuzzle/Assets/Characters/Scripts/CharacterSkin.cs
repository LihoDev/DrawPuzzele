using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterSkin : MonoBehaviour
{
    [SerializeField] private CharacterSkins _characterSkins;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();  
    }

    private void Start()
    {
        _animator.runtimeAnimatorController = _characterSkins.Sprite;
    }
}
