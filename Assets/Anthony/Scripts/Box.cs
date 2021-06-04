using UnityEngine;

[CreateAssetMenu(fileName = "Box", menuName = "Gacha/New Box", order = 101)]
public class Box : ScriptableObject
{
    
    [SerializeField] Collectibles[] collectiblesList;
    [SerializeField] int[] weightList ;
    [SerializeField] string boxName;
    [SerializeField] Box.Type boxType;
    //[SerializeField] int durability;
    //[SerializeField] Sprite boxSprite;
    public enum Type
    {
        Bronze,
        Silver,
        Gold
    }

    public Collectibles[] GetCollectibleList()
    {
        return collectiblesList;
    }
    public int[] GetWeightList()
    {
        return weightList;
    }
    /*
    public int GetDurability()
    {
        return durability;
    }
    public Sprite GetSprite()
    {
        return boxSprite;
    }
    public Box.Type GetBoxType()
    {
        return boxType;
    }

    public int BreakBox(int breakPower)
    {
        return durability - breakPower;
    }
    */


}
