using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonBase<TileManager>
{
    public Tile GetTile(float x, float y)
    {
        Vector2 position = new Vector2(x, y);

        ItemSO itemData = ItemManager.Instance.GetRandomItem();

        Tile tile = TilePool.Instance.GetTile();

        tile.SetItem(itemData);

        tile.SetPosition(position,0f);

        return tile;
    }

}
