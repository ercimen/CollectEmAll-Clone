using System.Collections.Generic;
using UnityEngine;

public class MatchManager : SingletonBase<MatchManager>
{
    private Tile[,] tiles;

    public void CheckMatches(Tile[,] tiles)
    {
        this.tiles = tiles;

        List<Tile> matchedTiles = new List<Tile>();

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y].GetState() != TileState.Matched)
                {
                    matchedTiles.Clear();

                    DFS(x, y, tiles[x, y].GetTileNumber(), matchedTiles);

                    if (matchedTiles.Count >= 3)
                    {
                        foreach (Tile tile in matchedTiles)
                        {
                            tile.Matched();
                        }
                    }
                }
            }
        }
    }

    private void DFS(int x, int y, int value, List<Tile> matchedTiles)
    {
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
        {
            return;
        }

        Tile currentTile = tiles[x, y];

        if (currentTile.GetState() == TileState.Matched || currentTile.GetTileNumber() != value)
        {
            return;
        }

        currentTile.SetState(TileState.Matched);
        matchedTiles.Add(currentTile);

        for (int i = x-1; i <= x+1; i++)
        {
            for (int j = y-1; j <= y+1; j++)
            {
                DFS(i, j, value, matchedTiles);
            }
        }

    }
}
