using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera;
    private Tile selectedTile;
    private bool isTileSelected = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider.TryGetComponent(out Tile hitTile))
            {
                    if (!isTileSelected)
                    {
                        selectedTile = hitTile;
                        isTileSelected = true;
                        Debug.LogWarning("first tile");
                    }
                    else
                    {
                        Debug.LogWarning("second tile");
                        float tileDistance = Mathf.Abs(selectedTile.transform.position.x - hitTile.transform.position.x) +
                            Mathf.Abs(selectedTile.transform.position.y - hitTile.transform.position.y);
                    Debug.LogWarning(tileDistance);

                    if (tileDistance == 1)
                        {
                        Debug.LogWarning(tileDistance);
                        GridManager.Instance.SwapTiles(selectedTile, hitTile,0.25f);
                        }
                        isTileSelected = false;
                    }
            }
        }
    }
}
