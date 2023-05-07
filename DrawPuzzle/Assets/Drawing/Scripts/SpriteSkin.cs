using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSkin : MonoBehaviour
{
    [SerializeField] private SpriteSkins _skins;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _skins.Sprite;
    }
}
