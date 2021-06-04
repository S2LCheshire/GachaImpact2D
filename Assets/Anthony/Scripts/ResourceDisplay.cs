using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplay : TextDisplay
{
    public enum ResourceType{
        GachaCoin,
        BronzeBox,
        SilverBox,
        GoldBox
    }
    [SerializeField] ResourceType resourceType;
    private void Start()
    {
        if (resourceType ==ResourceType.GachaCoin) InventoryManager.current.UpdateGachaCoin+= UpdateText;
        else if (resourceType == ResourceType.BronzeBox) InventoryManager.current.UpdateBronzeBox += UpdateText;
        else if (resourceType == ResourceType.SilverBox) InventoryManager.current.UpdateSilverBox += UpdateText;
        else if (resourceType == ResourceType.GoldBox) InventoryManager.current.UpdateGoldBox += UpdateText;
        InventoryManager.current.UpdateEvents();
    }
    private void OnDestroy()
    {
        if (resourceType == ResourceType.GachaCoin) InventoryManager.current.UpdateGachaCoin -= UpdateText;
        else if (resourceType == ResourceType.BronzeBox) InventoryManager.current.UpdateBronzeBox -= UpdateText;
        else if (resourceType == ResourceType.SilverBox) InventoryManager.current.UpdateSilverBox -= UpdateText;
        else if (resourceType == ResourceType.GoldBox) InventoryManager.current.UpdateGoldBox -= UpdateText;
    }
}
