using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : SingletonBase<GridManager>
{

    private Tile[,] tiles;

    public void InitializeGrid(LevelData levelData)
    {
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

    public void SwapTiles(Tile tile1, Tile tile2,float duration)
    {
        Vector2 tempPos = tile1.transform.position;
        tile1.SetPosition(tile2.transform.position, duration);
        tile2.SetPosition(tempPos, duration);
        tile1.SetState(TileState.Idle);

        Vector2Int tile1Index = GetTileIndex(tile1);
        Vector2Int tile2Index = GetTileIndex(tile2);
        tiles[tile1Index.x, tile1Index.y] = tile2;
        tiles[tile2Index.x, tile2Index.y] = tile1;
    }
    public void SwapTiles(Tile tile1, Tile tile2)
    {
        Vector2 tempPos = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempPos;

        Vector2Int tile1Index = GetTileIndex(tile1);
        Vector2Int tile2Index = GetTileIndex(tile2);
        tiles[tile1Index.x, tile1Index.y] = tile2;
        tiles[tile2Index.x, tile2Index.y] = tile1;
    }

    private Vector2Int GetTileIndex(Tile tile)
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] == tile)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return Vector2Int.zero;
    }

    public void ShuffleGrid()
    {
        StartCoroutine(ShuffleCoroutine());
        
        IEnumerator ShuffleCoroutine()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    int newX = UnityEngine.Random.Range(0, tiles.GetLength(0));
                    int newY = UnityEngine.Random.Range(0, tiles.GetLength(1));

                    Tile tile = tiles[i, j];
                    SwapTiles(tile, tiles[newX, newY]);

                    yield return null;
                }
            }
        }

       
    }

    public Tile[,] GetTiles()
    {
        return tiles;
    }
}
