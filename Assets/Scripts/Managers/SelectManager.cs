using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : SingletonBase<SelectManager>
{
    [SerializeField] private LayerMask _tileLayer;

    private Camera _mainCamera;

    private bool _onDown;

    private Tile _lastTile;

    private Dictionary<Tile, int> _selectedTiles = new();

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        InputMethods();
    }

    private void InputMethods()
    {
        if (Input.GetMouseButton(0))
        {
            SelectTile();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DeselectTiles();
        }
    }

    private void SelectTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, _tileLayer);

        if (hit.collider == null)
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out Tile hitTile)) return;

        if (!_onDown)
        {
            hitTile.Select();
            _onDown = true;
            _lastTile = hitTile;
            _selectedTiles.Add(hitTile,_selectedTiles.Count);
        }

        if (_lastTile == hitTile)
        {
            return;
        }

        int newValue;
        if (_selectedTiles.TryGetValue(hitTile, out newValue))
        {
            int oldValue;

            _selectedTiles.TryGetValue(_lastTile, out oldValue);

            if (newValue == oldValue-1)
            {
                _lastTile.UnSelect();
                _selectedTiles.Remove(_lastTile);
                _lastTile = hitTile;
                hitTile.Select();
                return;
            }

            return;
        }


        if (!hitTile.GetSelectStatus())
        {
            return;
        }

        if (hitTile.GetState()==TileState.Selected)
        {
            return;
        }

        Debug.LogWarning(hitTile.GetTileIndex() + " Tile Status=" + hitTile.GetState());

        hitTile.Select();
        hitTile.DrawLine(_lastTile.transform.position);
        _lastTile = hitTile;
        _selectedTiles.Add(hitTile, _selectedTiles.Count);
        _onDown = true;
    }

    private void DeselectTiles()
    {
        if (!_onDown)
        {
            return;
        }

        EventManager.Instance.Publish(CustomEvents.onSelectEnd);

        _onDown = false;

        if (_selectedTiles.Count >= 3)
        {
            MatchManager.Instance.MatchTiles(_selectedTiles);
        }

        _selectedTiles.Clear();
    }
}

