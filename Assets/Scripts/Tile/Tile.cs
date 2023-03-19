using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour, ISelectable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LineRenderer _lineRenderer;

    private ItemSO _item;
    private TileState _state = TileState.Idle;

    private bool _canSelect;
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

        switch (_state)
        {
            case TileState.Idle:
                IdleStateUpdate();
                break;
            case TileState.Selected:
                SelectedStateUpdate();
                break;
            case TileState.Matched:
                MatchedStateUpdate();
                break;
            default:
                break;
        }
    }

    private void IdleStateUpdate()
    {
        _spriteRenderer.color = Color.white;
        SetSelectStatus(false);
    }

    private void SelectedStateUpdate()
    {

    }

    private void MatchedStateUpdate()
    {

    }
    public void Matched()
    {
        _state = TileState.Matched;
        _spriteRenderer.color = Color.red;
    }

    public bool GetSelectStatus()
    {
        return _canSelect;
    }

    public void SetSelectStatus(bool value)
    {
        _canSelect = true;
    }
}

