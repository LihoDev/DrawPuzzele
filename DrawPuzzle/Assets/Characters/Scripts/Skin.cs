using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Skin : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController _animatorController;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();  
    }

    private void Start()
    {
        _animator.runtimeAnimatorController = _animatorController;
    }
}
