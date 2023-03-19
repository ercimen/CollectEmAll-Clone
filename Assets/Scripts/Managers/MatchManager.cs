using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MatchManager : SingletonBase<MatchManager>
{
    private Tile[,] tiles;

    public void CheckMatches(Tile[,] tiles)
    {
        this.tiles = tiles;

        List<Tile> matchedTiles = new List<Tile>();

        int matchedCount = 0;

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (!tiles[x, y].CanMatch)
                {
                    matchedTiles.Clear();

                    DFS(x, y, tiles[x, y].GetTileNumber(), matchedTiles);

                    if (matchedTiles.Count >= 3)
                    {
                        matchedCount++;
                    }
                }
            }

        }

        Debug.LogWarning("Matched Count: " + matchedCount);
        if (matchedCount == 0)
        {
            GridManager.Instance.ShuffleGrid();
        }
    }

    private void DFS(int x, int y, int value, List<Tile> matchedTiles)
    {
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
        {
            return;
        }

        Tile currentTile = tiles[x, y];

        if (currentTile.CanMatch || currentTile.GetTileNumber() != value)
        {
            return;
        }

        currentTile.CanMatch = true;
        matchedTiles.Add(currentTile);

        for (int i = x-1; i <= x+1; i++)
        {
            for (int j = y-1; j <= y+1; j++)
            {
                DFS(i, j, value, matchedTiles);
            }
        }
    }

    public void CheckNeighbors(Tile selectedTile)
    {
        EventManager.Instance.Publish(CustomEvents.onResetTiles);

        int2 tileIndex = selectedTile.GetTileIndex();

        Debug.LogWarning(tileIndex);      

        for (int i = tileIndex.x - 1; i <= tileIndex.x + 1; i++)
        {
            if (i < 0 || i >= tiles.GetLength(0)) continue;

            for (int j = tileIndex.y - 1; j <= tileIndex.y + 1; j++)
            { 
                if (j < 0  || j >= tiles.GetLength(1)) continue;
     
                if (selectedTile.GetTileNumber() ==tiles[i,j].GetTileNumber())
                {
                    tiles[i, j].SetSelectStatus(true);
                }
            }
        }
    }

    public void MatchTiles(Dictionary<Tile,int> matchedTiles)
    {
        foreach (KeyValuePair<Tile, int> tile in matchedTiles)
        {
            tile.Key.SetState(TileState.Matched);
            GridManager.Instance.RemoveTileAtGrid(tile.Key.GetTileIndex());
        }

        GridManager.Instance.ShiftTilesDown();
    }

}
