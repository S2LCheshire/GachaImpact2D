using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryTabButton : TabButtonWithEffect
{
    [SerializeField] Collectibles.Type type;
    [SerializeField] int index;
    [SerializeField] Collectibles collectibles;//REMOVE SERIALIAZABLE
    Image backgroundImage;
    [SerializeField] Image imageIcon;
    [SerializeField] Image blurIcon;
    [SerializeField] Sprite normalBackgroundSprite;
    [SerializeField] Sprite equippedBackgroundSprite;
    [SerializeField] TextMeshProUGUI ownedCountText;
    //[SerializeField] Button equipButton;
    bool isUnlocked = false;
    bool isEverUnlocked = false;
    bool isEquipped = false;
    InventoryTabController inventoryTabController;
    int ownedCount = 0;
    InventoryManager inventoryManager;


    protected override void Awake()
    {
        base.Awake();
        inventoryManager = FindObjectOfType<InventoryManager>();
        isStarted = false;
    }

    protected override void Start()
    {
        collectibles = inventoryManager.GetInventoryCollectiblesReference().GetCollectibles(type, index);
        backgroundImage = GetComponent<Image>();
        inventoryTabController = GetComponentInParent<InventoryTabController>();
        UpdateSelf();
        base.Start();
        //equipButton.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if (isStarted) UpdateSelf();
    }


    
    private void UpdateSelf()
    {
        if (type == Collectibles.Type.Hero && index < Inventory.MAX_HERO_COUNT)
        {
            if (inventoryManager.heroCollection[index] > 0)
            {
                isUnlocked = true;
                isEverUnlocked = true;
                if (inventoryManager.equippedHeroIndex == index) isEquipped = true;
                else isEquipped = false;
            }
            else
            {
                isUnlocked = false;
            }
            ownedCount = inventoryManager.heroCollection[index];

        }
        else if (type == Collectibles.Type.Pet && index < Inventory.MAX_PET_COUNT)
        {
            if (inventoryManager.petCollection[index] > 0)
            {
                isUnlocked = true;
                isEverUnlocked = true;
                if (inventoryManager.equippedPetIndex == index) isEquipped = true;
                else isEquipped = false;
            }
            else
            {
                isUnlocked = false;
            }

            ownedCount = inventoryManager.petCollection[index];

        }
        else if (type == Collectibles.Type.Item && index < Inventory.MAX_ITEM_COUNT)
        {
            if (inventoryManager.itemCollection[index] > 0)
            {
                isUnlocked = true;
                isEverUnlocked = true;
                if (inventoryManager.equippedItemIndex == index) isEquipped = true;
                else isEquipped = false;
            }
            else
            {
                isUnlocked = false;
            }

            ownedCount = inventoryManager.itemCollection[index];
        }
        if (isUnlocked)
        {
            blurIcon.enabled = false;
            imageIcon.sprite = collectibles.GetSprite();
            if (isEquipped)
            {
                backgroundImage.sprite = equippedBackgroundSprite;
                inventoryTabController.spriteDisplay.UpdateCollectibles(collectibles);
            }
        }
        else
        {
            blurIcon.enabled = true;
            if (isEverUnlocked) imageIcon.sprite = collectibles.GetSprite();
            else if (GameAssets.current != null) imageIcon.sprite = GameAssets.current.QuestionMark;
            //no animation
        }
        ownedCountText.text = "x" + ownedCount.ToString();
    }

    public override void UnselectTab()
    {
        isSelected = false;
        tabImage.color = tabColor;
        if (inventoryTabController.spriteDisplay!=null) inventoryTabController.spriteDisplay.UpdateEmptyCollectibles();
        //equipButton.gameObject.SetActive(false);
    }
    public override void SelectTab()
    {
        isSelected = true;
        tabImage.color = selectedTabColor;
        if (inventoryTabController.spriteDisplay != null)
        {
            if (isEverUnlocked)
            {
                inventoryTabController.spriteDisplay.UpdateCollectibles(collectibles);
                //equipButton.gameObject.SetActive(true);
            }
            else inventoryTabController.spriteDisplay.UpdateLockedCollectibles();
        }
    }

    public void ToggleEquip()
    {
        if (!isUnlocked) return;
        else
        {
            if (isEquipped) Unequip();
            else
            {
                foreach (InventoryTabButton itb in FindObjectsOfType<InventoryTabButton>()) itb.Unequip(); 
                Equip();
            }
        }
    }

    private void Equip()
    {
        //Put this in the event button
        //add border
        //Debug.Log("Equipping " + collectibles.name);
        if (type == Collectibles.Type.Hero && index < Inventory.MAX_HERO_COUNT)
        {
            inventoryManager.equippedHeroIndex = index;
        }
        else if (type == Collectibles.Type.Pet && index < Inventory.MAX_PET_COUNT)
        {
            inventoryManager.equippedPetIndex = index;
        }
        else if (type == Collectibles.Type.Item && index < Inventory.MAX_ITEM_COUNT)
        {
            inventoryManager.equippedItemIndex = index;
        }

        isEquipped = true;
        backgroundImage.sprite = equippedBackgroundSprite;
    }
    public void Unequip()
    {
        //Debug.Log("Unequipping " + collectibles.name);
        if (type == Collectibles.Type.Hero)
        {
            inventoryManager.equippedHeroIndex = -1;
        }
        else if (type == Collectibles.Type.Pet)
        {
            inventoryManager.equippedPetIndex = -1;
        }
        else if (type == Collectibles.Type.Item)
        {
            inventoryManager.equippedItemIndex = -1;
        }

        isEquipped = false;
        backgroundImage.sprite = normalBackgroundSprite;
    }
}
