using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonBase<InputManager>
{
    private Camera _mainCamera;

    private bool _onDown;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _onDown = true;
            SelectTile();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _onDown = false;
            DeselectTiles();
        }
    }

    private void SelectTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      
        if (hit.collider.TryGetComponent(out Tile hitTile))
        {
            Debug.LogWarning(hitTile.GetTileIndex());

            if (!_onDown)
            {
                hitTile.Select();
            }

            if (hitTile.GetSelectStatus())
            {
                hitTile.Select();                
            }
        }
    }

    private void DeselectTiles()
    {
        EventManager.Instance.Publish(CustomEvents.onResetTiles);
    }
}

