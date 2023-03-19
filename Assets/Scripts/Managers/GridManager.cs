using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


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
                int2 tileIndex =new int2(x, y);
                Tile tile = TileManager.Instance.GetTile(x, y);
                tile.SetTileIndex(tileIndex);
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

        int2 tile1Index = GetTileIndex(tile1);
        int2 tile2Index = GetTileIndex(tile2);
        int2 tempInt2 = tile1Index;
        tile1.SetTileIndex(tile2Index);
        tile2.SetTileIndex(tempInt2);

        tiles[tile1Index.x, tile1Index.y] = tile2;
        tiles[tile2Index.x, tile2Index.y] = tile1;
    }
    public void SwapTiles(Tile tile1, Tile tile2)
    {
        Vector2 tempPos = tile1.transform.position;
        tile1.transform.position = tile2.transform.position;
        tile2.transform.position = tempPos;

        tile1.SetState(TileState.Idle);
        tile2.SetState(TileState.Idle);

        int2 tile1Index = GetTileIndex(tile1);
        int2 tile2Index = GetTileIndex(tile2);
        int2 tempInt2 = tile1Index;

        tiles[tile1Index.x, tile1Index.y] = tile2;
        tiles[tile2Index.x, tile2Index.y] = tile1;

        tile1.SetTileIndex(tile2Index);
        tile2.SetTileIndex(tempInt2);
    }

    private int2 GetTileIndex(Tile tile)
    {
        return tile.GetTileIndex();
    }

    public void ShuffleGrid()
    {
        StartCoroutine(ShuffleCoroutine(ShufleCallback));

        IEnumerator ShuffleCoroutine(Action<bool> callback)
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

            yield return null;

            callback(true);
        }

    }

    public void ShufleCallback(bool isDone)
    {
        if (isDone) MatchManager.Instance.CheckMatches(tiles);        
    }

    public void ShiftTilesDown()
    {
        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                if (tiles[x, y] == null)
                {
                    for (int i = y + 1; i < tiles.GetLength(1); i++)
                    {
                        if (tiles[x, i] != null)
                        {
                            tiles[x, i].SetPosition(new Vector2(x, i - 1),0.2f);
                            tiles[x, i - 1] = tiles[x, i];
                            tiles[x, i] = null;
                        }
                    }
                }
            }
        }
    }


    public void RemoveTileAtGrid(int2 value)
    {
        tiles[value.x, value.y] = null;
    }

    public Tile[,] GetTiles()
    {
        return tiles;
    }

}
