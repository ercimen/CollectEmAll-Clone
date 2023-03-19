using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour, IHittable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private ItemSO _item;
    private TileState _state = TileState.Idle;

    public void SetPosition(Vector2 position, float duration)
    {
        transform.DOMove(position, duration);
    }

    public void SetItem(ItemSO item)
    {
        _item = item;
        SetSprite();
    }

    private void SetSprite()
    {
        _spriteRenderer.sprite = _item.GetSprite();
    }

    public void GetHit()
    {
        _spriteRenderer.color = Color.red;
    }

    public int GetTileNumber()
    {
        return _item.GetValue();
    }

    public TileState GetState()
    {
        return _state;
    }

    public void SetState(TileState state)
    {
        _state = state;
    }

    public void Matched()
    {
        _state = TileState.Matched;
        _spriteRenderer.color = Color.red;
    }
}

