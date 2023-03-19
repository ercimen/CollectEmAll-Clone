using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System;

public class Tile : MonoBehaviour, ISelectable
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LineRenderer _lineRenderer;

    private ItemSO _item;
    private TileState _state = TileState.Idle;
    private bool _canSelect;

    private int2 _tileIndex;

    private void OnEnable()
    {
        EventManager.Instance.Subscribe(CustomEvents.onResetTiles, ResetTile);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnSubscribe(CustomEvents.onResetTiles, ResetTile);
    }

    private void ResetTile(object[] arguments)
    {
        SetState(TileState.Idle);
        SetSelectStatus(false);
    }

    public int2 GetTileIndex()
    {
        return _tileIndex;
    }
    public void SetTileIndex(int2 tileIndex)
    {
        _tileIndex = tileIndex;
    }

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

    public void Select()
    {
        SetState(TileState.Selected);
        MatchManager.Instance.CheckNeighbors(this);

#if UNITY_EDITOR
        _spriteRenderer.color = Color.black;
#endif
    }

    public void UnSelect()
    {
        SetState(TileState.Idle);
        SetSelectStatus(false);
        DrawLine(transform.position);
    }

    public void DrawLine(Vector2 position)
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, position);
    }
    public bool GetSelectStatus()
    {
        return _canSelect;
    }

    public void SetSelectStatus(bool value)
    {
        _canSelect = value;
    }
}


