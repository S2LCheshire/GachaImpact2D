using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_SAVE_KEY = "master save";
    const string DIFFICULTY_KEY = "difficult";


    //const int MIN_ITEM_ID = 1;
    //const int MAX_ITEM_ID = 32;

    public static void SetItem(Collectibles collectibles, bool hasIt)
    {
        string stringID = collectibles.GetItemType().ToString()[0] + collectibles.GetRarity().ToString()[0] + "-" + collectibles.GetItemID().ToString().PadLeft(2, '0');

    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SAVE_KEY);
    }

    public static bool DoesSaveFileExist()
    {
        if (PlayerPrefs.HasKey(MASTER_SAVE_KEY)) return true;
        else return false;
    }
}
