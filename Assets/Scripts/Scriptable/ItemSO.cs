using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int value;

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetValue()
    {
        return value;
    }
}
