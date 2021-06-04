using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField] TextDisplay bronzeBoxCostDisplay;
    [SerializeField] TextDisplay silverBoxCostDisplay;
    [SerializeField] TextDisplay goldBoxCostDisplay;
    bool isStarted= false;
    public void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        isStarted = false;
    }
    public void OnEnable()
    {
        if (isStarted) UpdateCostDisplay();
    }

    public void Start()
    {
        UpdateCostDisplay();
        isStarted = true;
    }

    private void UpdateCostDisplay()
    {
        bronzeBoxCostDisplay.UpdateText(inventoryManager.bronzeBoxCost);
        silverBoxCostDisplay.UpdateText(inventoryManager.silverBoxCost);
        goldBoxCostDisplay.UpdateText(inventoryManager.goldenBoxCost);
    }

    public void BuyBronzeBox()
    {
        inventoryManager.BuyBronzeBox();
    }
    public void BuySilverBox()
    {
        inventoryManager.BuySilverBox();
    }
    public void BuyGoldBox()
    {
        inventoryManager.BuyGoldBox();
    }

}
