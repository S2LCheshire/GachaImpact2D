using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectibles", menuName = "Gacha/New Collectibles", order = 100)]
public class Collectibles : ScriptableObject
{
    public enum Rarity
    {
        Common,
        Rare,
        SuperRare,
        UltraRare
    }
    public enum Type
    {
        Hero,
        Pet,
        Item
    }

    [SerializeField] string itemName;
    [SerializeField] int itemID;
    [SerializeField] Type itemType;
    [SerializeField] Rarity itemRarity;
    [SerializeField] Sprite itemSprite;
    [SerializeField] RuntimeAnimatorController itemAnimatorController;
    [SerializeField] string itemEffectDescription;
    [SerializeField] int extraLife = 0;
    [SerializeField] int healLife = 0;
    [SerializeField] int extraCoin = 0;
    [SerializeField] float coinMultiplier = 0;
    [SerializeField] int zoomMultiplier = 1;
    [SerializeField] bool firstLetterHelper = false;

    public int GetExtraLife()
    {
        return extraLife;
    }
    public float GetCoinMultiplier()
    {
        return coinMultiplier;
    }
    public int GetExtraCoin()
    {
        return extraCoin;
    }

    public string GetItemName()
    {
        return itemName;
    }
    public int GetItemID()
    {
        return itemID;
    }
    public Type GetItemType()
    {
        return itemType;
    }
    public Rarity GetRarity()
    {
        return itemRarity;
    }
    public Sprite GetSprite()
    {
        return itemSprite;
    }
    public RuntimeAnimatorController GetItemAnimatorController()
    {
        return itemAnimatorController;
    }
    public string GetItemEffect()
    {
        return itemEffectDescription;
    }


}
