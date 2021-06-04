
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager current;

    public delegate void UpdateQuantity(int quantity);

    const int MAX_SPECIAL_CONDITION = 10;

    [Header("Gacha Coins")]
    public int gachaCoin = 0;
    public UpdateQuantity UpdateGachaCoin;

    [Header("Boxes")]
    public int bronzeBox = 0;
    public int bronzeBoxCost = 500;
    public UpdateQuantity UpdateBronzeBox;
    public int silverBox =0;
    public int silverBoxCost = 2000;
    public UpdateQuantity UpdateSilverBox;
    public int goldenBox =0;
    public int goldenBoxCost = 5000;
    public UpdateQuantity UpdateGoldBox;

    [Header("Collections")]
    public int[] heroCollection;
    public int[] petCollection;
    public int[] itemCollection;
    public int equippedHeroIndex;
    public int equippedPetIndex;
    public int equippedItemIndex;


    [Header("Special Condition")]
    public bool[] specialCondition;
    /// Special condition
    /// 0 --> first perfect bronze = Guaranteed Rare
    /// 1 --> first perfect silver = Guranteed Super Rare
    /// 2 --> first perfect gold = Guaranteed Non Hero - Ultra Rare
    /// 3 --> perfect bronze with 3 heroes = Hero UR
    /// 4 --> First opened 20th bronze in a row = Hero SR
    /// 

    InventoryCollectiblesReference inventoryCollectiblesReference;
    AudioManager audioManager;

    //private delegate void UpdateGachaCoinDelegate(int i); 
    //public Action<int> UpdateGachaCoin;

    private void Awake()
    {
        /*
        InventoryManager[] inventoryManagers = FindObjectsOfType<InventoryManager>();
        if (inventoryManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        */
        inventoryCollectiblesReference = GetComponentInChildren<InventoryCollectiblesReference>();
        audioManager = FindObjectOfType<AudioManager>();
        current = this;
        Initialize();
        LoadInventory();
    }


    private void Start()
    {
        //Popup.Create(transform.position, 10);
    }

    public void SaveInventory()
    {
        SaveLoadSystem.SaveInventory(this);
    }
    public void LoadInventory()
    {
        Inventory inventory = SaveLoadSystem.LoadInventory();
        gachaCoin = inventory.gachaCoin;
        bronzeBox = inventory.bronzeBox;
        silverBox = inventory.silverBox;
        goldenBox = inventory.goldenBox;
        heroCollection = inventory.heroCollection;
        petCollection = inventory.petCollection;
        itemCollection = inventory.itemCollection;
        equippedHeroIndex = inventory.equippedHeroIndex;
        equippedPetIndex = inventory.equippedPetIndex;
        equippedItemIndex = inventory.equippedItemIndex;
        specialCondition = inventory.specialCondition;
        UpdateEvents();
    }
    public void ResetInventory()
    {
        Initialize();
        UpdateEvents();
        //SaveLoadSystem.SaveInventory(this);

    }
    public void Initialize()
    {
        gachaCoin = 0;
        bronzeBox = 0;
        silverBox = 0;
        goldenBox = 0;
        heroCollection = new int[Inventory.MAX_HERO_COUNT];
        petCollection = new int[Inventory.MAX_PET_COUNT];
        itemCollection = new int[Inventory.MAX_ITEM_COUNT];
        equippedHeroIndex = -1;
        equippedPetIndex = -1;
        equippedItemIndex = -1;
        specialCondition = new bool[MAX_SPECIAL_CONDITION];
        UpdateEvents();
    }
    public void UpdateEvents()
    {

        UpdateGachaCoin?.Invoke(gachaCoin);
        UpdateBronzeBox?.Invoke(bronzeBox);
        UpdateSilverBox?.Invoke(silverBox);
        UpdateGoldBox?.Invoke(goldenBox);
    }
    /*
    public static void GetItemID(Collectibles collectibles, bool hasIt)
    {
        string stringID = collectibles.GetItemType().ToString()[0] + collectibles.GetRarity().ToString()[0] + "-" + collectibles.GetItemID().ToString().PadLeft(2, '0');
    }
    */

    public int GetCollectiblesOwnedCount(Collectibles.Type collType)
    {
        if (collType == Collectibles.Type.Hero) return heroCollection.Count(n => n > 0);
        else if (collType == Collectibles.Type.Pet) return petCollection.Count(n => n > 0);
        else if (collType == Collectibles.Type.Item) return itemCollection.Count(n => n > 0);
        else return -1;
    }

    public InventoryCollectiblesReference GetInventoryCollectiblesReference()
    {
        return inventoryCollectiblesReference;
    }
    public void AddGachaCoin(int coinAdded)
    {
        if (coinAdded > 0)
        {
            gachaCoin += coinAdded;
            //PopUp.Create(transform.position, "+"+coinAdded.ToString());
        }
        if (UpdateGachaCoin != null)
        {
            UpdateGachaCoin.Invoke(gachaCoin);
        }
        SaveInventory();
    }
    public bool SpendGachaCoin(int coinUsed)
    {
        if (coinUsed > gachaCoin)
        {
            //Popup.Create(transform.position, "Not enough coin!");
            DialogueBox.current.TypeSentence("Not enough Gacha Coin!");
            audioManager.Play("NotEnoughResource");
            return false;
        }
        else
        {
            gachaCoin -= coinUsed;
            if (UpdateGachaCoin != null) UpdateGachaCoin.Invoke(gachaCoin);
            SaveInventory();
            return true;
        }
    }

    public int GetGachaCoin()
    {
        return gachaCoin;
    }

    public void BuyBronzeBox()
    {
        if (SpendGachaCoin(bronzeBoxCost))
        {
            AddBronzeBox(1);
            DialogueBox.current.TypeSentence("Thank you for purchasing a Bronze Box!");
        }
           
    }
    public void BuySilverBox()
    {
        if (SpendGachaCoin(silverBoxCost))
        {
            AddSilverBox(1);
            DialogueBox.current.TypeSentence("Thank you for purchasing a Silver Box!");
        }
    }
    public void BuyGoldBox()
    {
        if (SpendGachaCoin(goldenBoxCost))
        {
            AddGoldBox(1);
            DialogueBox.current.TypeSentence("Thank you for purchasing a Gold Box!");
        }
    }

    public void AddBronzeBox(int boxAdded)
    {
        AddBox(Box.Type.Bronze, boxAdded);
    }
    public void AddSilverBox(int boxAdded)
    {
        AddBox(Box.Type.Silver, boxAdded);
    }
    public void AddGoldBox(int boxAdded)
    {
        AddBox(Box.Type.Gold, boxAdded);
    }
    public void UseBronzeBox(int boxUsed)
    {
        UseBox(Box.Type.Bronze, boxUsed);
    }
    public void UseSilverBox(int boxUsed)
    {
        UseBox(Box.Type.Silver, boxUsed);
    }
    public void UseGoldBox(int boxUsed)
    {
        UseBox(Box.Type.Gold, boxUsed);
    }

    public void AddBox(Box.Type boxType, int boxAdded)
    {
        if (boxAdded < 0) 
        {
            //Debug.LogWarning("negative number input - no box was added");
            return;
        }
        if (boxType == Box.Type.Bronze)
        {
            bronzeBox += boxAdded;
            if (UpdateBronzeBox != null) UpdateBronzeBox.Invoke(bronzeBox);
        }
        else if (boxType == Box.Type.Silver)
        {
            silverBox += boxAdded;
            if (UpdateSilverBox != null) UpdateSilverBox.Invoke(silverBox);
        }
        else if (boxType == Box.Type.Gold)
        {
            goldenBox += boxAdded;
            if (UpdateGoldBox != null) UpdateGoldBox.Invoke(goldenBox);
        }
        audioManager.Play("BuyBox");
        SaveInventory();
    }
    public bool UseBox(Box.Type boxType, int boxUsed)
    {
        if (boxUsed < 0)
        {
            ////Debug.LogWarning("negative number input - no box was used");
            DialogueBox.current.TypeSentence("Negative number input - no box was used");
            return false;
        }
        if (boxType == Box.Type.Bronze)
        {
            if (bronzeBox - boxUsed < 0)
            {
                DialogueBox.current.TypeSentence("Not enough Bronze Box");
                audioManager.Play("NotEnoughResource");
                return false;
            }
            else
            {
                bronzeBox -= boxUsed;
                if (UpdateBronzeBox != null) UpdateBronzeBox.Invoke(bronzeBox);

                SaveInventory();
                return true;
            }
        }
        else if (boxType == Box.Type.Silver)
        {
            if (silverBox - boxUsed < 0)
            {
                DialogueBox.current.TypeSentence("Not enough Silver Box");
                audioManager.Play("NotEnoughResource");
                return false;
            }
            else
            {
                silverBox -= boxUsed;
                if (UpdateSilverBox != null) UpdateSilverBox.Invoke(silverBox);

                SaveInventory();
                return true;
            }
        }
        else if (boxType == Box.Type.Gold)
        {
            if (goldenBox - boxUsed < 0) 
            {
                DialogueBox.current.TypeSentence("Not enough Gold Box");
                audioManager.Play("NotEnoughResource");
                return false;
            } 
            else
            {
                goldenBox -= boxUsed;
                if (UpdateGoldBox != null) UpdateGoldBox.Invoke(goldenBox);

                SaveInventory();
                return true;
            }
        }
        return false;
    }

    public void EquipCollectibles(Collectibles collectibles)
    {
        return;
    }


    public void ChangeCollectibles(Collectibles collectibles, int change)
    {
        Collectibles.Type collectiblesType = collectibles.GetItemType();
        int index = inventoryCollectiblesReference.GetCollectiblesIndex(collectibles);
        if (collectiblesType == Collectibles.Type.Hero && heroCollection[index] + change >=0) heroCollection[index] += change;
        else if (collectiblesType == Collectibles.Type.Pet && petCollection[index] + change >= 0) petCollection[index] += change;
        else if (collectiblesType == Collectibles.Type.Item && itemCollection[index] + change >= 0) itemCollection[index] += change;

        SaveInventory();
    }
    public void AddCollectibles(Collectibles collectibles)
    {
        ChangeCollectibles(collectibles, 1);
    }
    public void UseCollectibles(Collectibles collectibles)
    {
        ChangeCollectibles(collectibles, -1);
    }

    public Collectibles GetEquippedHero()
    {

        if (equippedHeroIndex < 0)
        {
            return null;
        }
        else
        {
            return  GetInventoryCollectiblesReference().GetCollectibles(Collectibles.Type.Hero, equippedHeroIndex);
        }
            
    }

    public Collectibles GetEquippedPet()
    {

        if (equippedPetIndex < 0)
        {
            return null;
        }
        else
        {
            return GetInventoryCollectiblesReference().GetCollectibles(Collectibles.Type.Pet, equippedPetIndex);
        }

    }

    public Collectibles GetEquippedItem()
    {

        if (equippedItemIndex < 0)
        {
            return null;
        }
        else
        {
            return GetInventoryCollectiblesReference().GetCollectibles(Collectibles.Type.Item, equippedItemIndex);
        }

    }

}
