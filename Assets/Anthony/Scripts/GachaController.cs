using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GachaController : MonoBehaviour
{
    Box currentBox;
    [SerializeField] TabController gachaTabController;
    [SerializeField] BoxSelector boxSelector;
    [SerializeField] PowerSelector powerSelector;
    [SerializeField] SpriteChanger spriteDisplay;
    [SerializeField] Box[] bronzeBoxList;
    [SerializeField] Box[] silverBoxList;
    [SerializeField] Box[] goldBoxList;
    [SerializeField] Box[] specialBoxList;
    InventoryManager inventoryManager;
    /// Box List
    /// 0 --> Very Bad
    /// 1 --> Bad
    /// 2 --> Base
    /// 3 --> Good
    /// 4 --> Perfect --> next box base
    Collectibles[] collectibleList;
    /// Special Condition
    /// 0 --> first perfect bronze = Guaranteed Rare
    /// 1 --> first perfect silver = Guranteed Super Rare
    /// 2 --> first perfect gold = Guaranteed Non Hero - Ultra Rare
    /// 3 --> perfect bronze with 3 heroes = Hero UR
    /// 4 --> First opened 20th bronze in a row = Hero SR
    /// 5
    /// </summary>
    int[] weightList;
    
    int collectiblesLength;
    int weightLength;

    const int upperBound1 = 1;
    const int upperBound2 = 10;
    const int upperBound3 = 20;


    const int baseBronzeBoxDurability = 10;
    const int baseSilverBoxDurability = 30;
    const int baseGoldBoxDurability = 50;

    public bool inGacha = false;

    AudioManager audioManager;
    //public Event testing;

    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ChangeBox(Box box)
    {
        currentBox = box;
        collectibleList = box.GetCollectibleList();
        weightList = box.GetWeightList();
        collectiblesLength = collectibleList.Length;
        weightLength = weightList.Length;
        audioManager.Play("ChangeBox");
    }
    private int GetBaseBoxDurability(Box.Type boxType)
    {
        if (boxType == Box.Type.Bronze) return baseBronzeBoxDurability;
        else if (boxType == Box.Type.Silver) return baseSilverBoxDurability;
        else if (boxType == Box.Type.Gold) return baseGoldBoxDurability;
        else return -1;
    }
    private int RandomizeBoxDurability(Box.Type boxType)
    {
        int baseDurabilty = GetBaseBoxDurability(boxType);
        int variance = baseDurabilty / 5;
        int randomDurability = Random.Range(baseDurabilty - variance, baseDurabilty + variance+1);
        return randomDurability;
    }

    private Box DetermineBox(Box.Type boxType, int power)
    {
        int boxDurability = RandomizeBoxDurability(boxType);
        int difference = power - boxDurability;
        ////Debug.Log("Box Durability: " + boxDurability);
        ////Debug.Log("Power: " + power);
        ////Debug.Log("Difference: " + difference);

        Box returnBox = null;
        if (boxType == Box.Type.Bronze) returnBox = BronzeBoxChooser(difference);
        else if (boxType == Box.Type.Silver) returnBox = SilverBoxChooser(difference);
        else if (boxType == Box.Type.Gold) returnBox = GoldBoxChooser(difference);

        return returnBox;
    }

    private Box BronzeBoxChooser(int difference)
    {
        int index = -1;


        if (difference == 0)
        {
            //PERFECT
            DialogueBox.current.TypeSentence("That was a perfect hit!");
            index = 4;
        }
        else if (difference <= -upperBound3)
        {
            //way too weak
            DialogueBox.current.TypeSentence("sigh that was way too weak.. I guess I'll just give you this instead");
            index = 0;
        }
        else if (difference <= -upperBound2)
        {
            //too weak
            DialogueBox.current.TypeSentence("You only just about breaking it - try putting more power next time!");
            index = 1;
        }
        else if (difference < -upperBound1)
        {
            //slightly too weak
            DialogueBox.current.TypeSentence("It cracks but lacking the oomph");
            index = 2;
        }

        else if (difference <= upperBound1)
        {
            //Great!
            DialogueBox.current.TypeSentence("Great power and control!");
            index = 3;
        }
        else if (difference <= upperBound2)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("You hit it slightly too hard!");
            index = 2;
        }
        else if (difference <= upperBound3)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("Wow, that's too hard, buddy!");
            index = 1;
        }
        else
        {
            //too strong
            DialogueBox.current.TypeSentence("Are you crazy?? That was way too strong!");
            index = 0;
        }

        Box returnBox = null;
        if (index != -1) returnBox = bronzeBoxList[index];
        return returnBox;
    }
    private Box SilverBoxChooser(int difference)
    {
        int index = -1;


        if (difference == 0)
        {
            //PERFECT
            DialogueBox.current.TypeSentence("That was a perfect hit!");
            index = 4;
        }
        else if (difference <= -upperBound3)
        {
            //way too weak
            DialogueBox.current.TypeSentence("sigh that was way too weak.. I guess I'll just give you this instead");
            index = 0;
        }
        else if (difference <= -upperBound2)
        {
            //too weak
            DialogueBox.current.TypeSentence("You only just about breaking it - try putting more power next time!");
            index = 1;
        }
        else if (difference < -upperBound1)
        {
            //slightly too weak
            DialogueBox.current.TypeSentence("It cracks but lacking the oomph");
            index = 2;
        }

        else if (difference <= upperBound1)
        {
            //Great!
            DialogueBox.current.TypeSentence("Great power and control!");
            index = 3;
        }
        else if (difference <= upperBound2)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("You hit it slightly too hard!");
            index = 2;
        }
        else if (difference <= upperBound3)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("Wow, that's too hard, buddy!");
            index = 1;
        }
        else
        {
            //too strong
            DialogueBox.current.TypeSentence("Are you crazy?? That was way too strong!");
            index = 0;
        }

        Box returnBox = null;
        if (index != -1) returnBox = silverBoxList[index];
        return returnBox;
    }
    private Box GoldBoxChooser(int difference)
    {
        int index = -1;


        if (difference == 0)
        {
            //PERFECT
            DialogueBox.current.TypeSentence("That was a perfect hit!");
            index = 4;
        }
        else if (difference <= -upperBound3)
        {
            //way too weak
            DialogueBox.current.TypeSentence("sigh that was way too weak.. I guess I'll just give you this instead");
            index = 0;
        }
        else if (difference <= -upperBound2)
        {
            //too weak
            DialogueBox.current.TypeSentence("You only just about breaking it - try putting more power next time!");
            index = 1;
        }
        else if (difference < -upperBound1)
        {
            //slightly too weak
            DialogueBox.current.TypeSentence("It cracks but lacking the oomph");
            index = 2;
        }

        else if (difference <= upperBound1)
        {
            //Great!
            DialogueBox.current.TypeSentence("Great power and control!");
            index = 3;
        }
        else if (difference <= upperBound2)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("You hit it slightly too hard!");
            index = 2;
        }
        else if (difference <= upperBound3)
        {
            //slightly too strong
            DialogueBox.current.TypeSentence("Wow, that's too hard, buddy!");
            index = 1;
        }
        else
        {
            //too strong
            DialogueBox.current.TypeSentence("Are you crazy?? That was way too strong!");
            index = 0;
        }

        Box returnBox = null;
        if (index != -1) returnBox = goldBoxList[index];
        return returnBox;
    }
    public void Gacha()
    {
        if (inGacha) return;
        if (TryUseBox(boxSelector.GetBoxType()))
        {
            GachaLock();
            StartCoroutine(ChangeGachaBoxAndCallGachaLogic(2f));
            if (collectiblesLength != weightLength)
            {
                ////Debug.LogError("Collectible list and weight list not of the same length!");
                return;
            }
        }
        else return;
    }

    private IEnumerator ChangeGachaBoxAndCallGachaLogic(float waitTime)
    {
        ChangeBox(DetermineBox(boxSelector.GetBoxType(), powerSelector.GetBreakPower()));
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(GachaLogic());
    }

    public bool TryUseBox(Box.Type boxType)
    {
        return inventoryManager.UseBox(boxType, 1);
    }
    public IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    private IEnumerator GachaLogic()
        
    {
        float randomFloat = Random.Range(0f, 1f);
        ////Debug.Log("random number is: " + randomFloat.ToString());
        int weightSum = weightList.Sum();
        float currChance = 0f;
        for (int i = 0; i < collectiblesLength; i++)
        {
            float weightedChance = (float)weightList[i]/weightSum;
            ////Debug.Log("weighted chance is: " + weightedChance.ToString());
            ////Debug.Log("Current Low Bound: " + currChance);
            currChance += weightedChance;
            ////Debug.Log("Current Upper Bound: " + currChance);
        if (randomFloat <= currChance)
        {
            Collectibles collectiblesToReceive = collectibleList[i];
            ////Debug.Log("you get the item: " + collectiblesToReceive);
            DialogueBox.current.TypeSentence("You got the " + collectiblesToReceive.GetRarity()+" " +collectiblesToReceive.GetItemType().ToString().ToLower() + ", " + collectiblesToReceive.GetItemName() + ".\nCongratulations!!");
            inventoryManager.AddCollectibles(collectiblesToReceive);
            if (spriteDisplay != null)
            {
                spriteDisplay.ChangeSprite(collectiblesToReceive.GetSprite());
                spriteDisplay.LockSpriteChanger();
                audioManager.Play(ChooseGachaSFX(collectiblesToReceive.GetRarity()));
                yield return new WaitForSeconds(2f);
                spriteDisplay.UnlockSpriteChanger();
                spriteDisplay.UndoChangeSprite();
            }// play sfx?
            GachaUnlock();
            inventoryManager.SaveInventory();
            DialogueBox.current.TypeSentence("What would you like to do now?");
            break;
            }
        }
        //return collectibleList[randomInt];

    }
    private string ChooseGachaSFX(Collectibles.Rarity rarity)
    {
        if (rarity == Collectibles.Rarity.Common) return "OpenGachaCommon";
        else if (rarity == Collectibles.Rarity.Rare) return "OpenGachaRare";
        else if (rarity == Collectibles.Rarity.SuperRare) return "OpenGachaSuperRare";
        else if (rarity == Collectibles.Rarity.UltraRare) return "OpenGachaSuperRare";
        else return "";
    }

    private void GachaLock()
    {
        ////Debug.Log("locking..");
        inGacha = true;
        ScreenBlocker.current.TurnOn();
        gachaTabController.DeactivateKey();
    }
    private void GachaUnlock()
    {
        ////Debug.Log("unlocking..");
        inGacha = false;
        ScreenBlocker.current.TurnOff();
        StartCoroutine(gachaTabController.ActivateKey());
    }
}

