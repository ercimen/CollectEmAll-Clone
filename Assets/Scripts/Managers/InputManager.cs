using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonBase<InputManager>
{
    [SerializeField] private LayerMask _tileLayer;

    private Camera _mainCamera;

    private bool _onDown;

    private Tile _lastTile;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
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
        }

        if (_lastTile == hitTile)
        {
            return;
        }

        if (!hitTile.GetSelectStatus())
        {
            return;
        }

        Debug.LogWarning(hitTile.GetTileIndex() + " Tile Status=" + hitTile.GetState());

        hitTile.Select();
        _lastTile = hitTile;
        _onDown = true;

    }

    private void DeselectTiles()
    {
        if (!_onDown)
        {
            return;
        }

        EventManager.Instance.Publish(CustomEvents.onResetTiles);

        _onDown = false;
    }
}

