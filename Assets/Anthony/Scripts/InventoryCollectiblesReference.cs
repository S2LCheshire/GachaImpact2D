using System;
using UnityEngine;

public class InventoryCollectiblesReference: MonoBehaviour
{
    public Collectibles[] heroCollection = new Collectibles[Inventory.MAX_HERO_COUNT];
    public Collectibles[] petCollection = new Collectibles[Inventory.MAX_PET_COUNT];
    public Collectibles[] itemCollection = new Collectibles[Inventory.MAX_ITEM_COUNT];

    public int GetCollectiblesIndex(Collectibles collectibles)
    {
        if (collectibles.GetItemType() == Collectibles.Type.Hero) return Array.IndexOf(heroCollection, collectibles);
        else if (collectibles.GetItemType() == Collectibles.Type.Pet) return Array.IndexOf(petCollection, collectibles);
        else if (collectibles.GetItemType() == Collectibles.Type.Item) return Array.IndexOf(itemCollection, collectibles);
        else return -1;
    }
    public Collectibles GetCollectibles(Collectibles.Type type, int index)
    {
        if (type == Collectibles.Type.Hero && index< Inventory.MAX_HERO_COUNT) return heroCollection[index];
        else if (type == Collectibles.Type.Pet && index < Inventory.MAX_PET_COUNT) return petCollection[index];
        else if (type == Collectibles.Type.Item && index < Inventory.MAX_ITEM_COUNT) return itemCollection[index];
        else return null;
    }
}
