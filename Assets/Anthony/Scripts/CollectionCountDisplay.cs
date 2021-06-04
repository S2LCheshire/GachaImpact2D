using TMPro;
using UnityEngine;

public class CollectionCountDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] Collectibles.Type collectiblesType;
    InventoryManager inventoryManager;
  
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    private void OnEnable()
    {
        //if (InventoryManager.current == null) return;
        UpdateSelf();

    }

    private void Start()
    {
        UpdateSelf();
    }
    void UpdateSelf()
    {
        if (inventoryManager.GetCollectiblesOwnedCount(collectiblesType) == -1) return;
        string updatedText = collectiblesType + "\n" + inventoryManager.GetCollectiblesOwnedCount(collectiblesType) + "/";
        if (collectiblesType == Collectibles.Type.Hero) updatedText += Inventory.MAX_HERO_COUNT;
        else if (collectiblesType == Collectibles.Type.Pet) updatedText += Inventory.MAX_PET_COUNT;
        else if (collectiblesType == Collectibles.Type.Item) updatedText += Inventory.MAX_ITEM_COUNT;
        text.text = updatedText;
    }
}
