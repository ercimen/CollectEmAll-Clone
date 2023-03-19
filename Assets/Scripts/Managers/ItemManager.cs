using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [SerializeField] private ItemSO[] items;
    public ItemSO GetRandomItem()
    {
        return items[Random.Range(0, items.Length)];
    }
}
