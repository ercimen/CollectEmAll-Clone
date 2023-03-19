using System;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : SingletonBase<GridManager>
{
    [SerializeField] private int gridSize = 6;

    private Tile[,] tiles;

    public void InitializeGrid(LevelData levelData)
    {
        gridSize = levelData.column;

        tiles = new Tile[levelData.column, levelData.row];

        for (int x = 0; x < levelData.column; x++)
        {
            for (int y = 0; y < levelData.row; y++)
            {
                Tile tile = TileManager.Instance.GetTile(x, y);
               
                tiles[x, y] = tile;
            }
        }
        MatchManager.Instance.CheckMatches(tiles);
        CameraManager.Instance.AutoPositionCamera(new Vector2(levelData.column, levelData.row));
    }

    public void SwapTiles(Tile tile1, Tile tile2)
    {

        Vector2 tempPos = tile1.transform.position;
        tile1.SetPosition(tile2.transform.position, 0.25f);
        tile2.SetPosition(tempPos, 0.25f);

        Vector2Int tile1Index = GetTileIndex(tile1);
        Vector2Int tile2Index = GetTileIndex(tile2);
        tiles[tile1Index.x, tile1Index.y] = tile2;
        tiles[tile2Index.x, tile2Index.y] = tile1;

        MatchManager.Instance.CheckMatches(tiles);
    }

    private Vector2Int GetTileIndex(Tile tile)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (tiles[x, y] == tile)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return Vector2Int.zero;
    }

  public Tile[,] GetTiles()
    {
        return tiles;
    }
}
