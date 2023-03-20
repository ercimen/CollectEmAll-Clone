using System.Collections.Generic;
using UnityEngine;

public class TilePool : SingletonBase<TilePool>
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private int initialPoolSize = 500;

    private Queue<Tile> tilePool = new();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddTileToPool();
        }
    }

    private void AddTileToPool()
    {
        Tile tile = Instantiate(tilePrefab, transform);   
        tile.gameObject.SetActive(false);
        tilePool.Enqueue(tile);
    }

    public Tile GetTile()
    {
        if (tilePool.Count == 0)
        {
            AddTileToPool();
        }

        Tile tile = tilePool.Dequeue();
        tile.gameObject.SetActive(true);
        return tile;
    }

    public void ReturnTileToPool(Tile tile)
    {
        tilePool.Enqueue(tile);
        tile.gameObject.SetActive(false);
    }
}