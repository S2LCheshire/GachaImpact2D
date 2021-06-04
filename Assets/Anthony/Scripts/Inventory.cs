
using UnityEngine;


[System.Serializable]
public class Inventory 
{
    public const int MAX_HERO_COUNT = 4;
    public const int MAX_PET_COUNT = 20;
    public const int MAX_ITEM_COUNT = 8;
    public int gachaCoin;
    public int bronzeBox;
    public int silverBox;
    public int goldenBox;
    public int[] heroCollection;
    public int[] petCollection;
    public int[] itemCollection;
    public int equippedHeroIndex;
    public int equippedPetIndex;
    public int equippedItemIndex;
    public bool[] specialCondition;

    public Inventory(InventoryManager inventoryManager)
    {
        gachaCoin = inventoryManager.gachaCoin;
        bronzeBox = inventoryManager.bronzeBox;
        silverBox = inventoryManager.silverBox;
        goldenBox = inventoryManager.goldenBox;
        heroCollection = inventoryManager.heroCollection;
        petCollection = inventoryManager.petCollection;
        itemCollection = inventoryManager.itemCollection;
        equippedHeroIndex = inventoryManager.equippedHeroIndex;
        equippedPetIndex = inventoryManager.equippedPetIndex;
        equippedItemIndex = inventoryManager.equippedItemIndex;
        specialCondition = inventoryManager.specialCondition;
}
}
