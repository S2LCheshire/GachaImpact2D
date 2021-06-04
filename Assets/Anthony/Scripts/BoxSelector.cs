using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelector : MonoBehaviour
{
    [SerializeField]SpriteChanger boxDisplay;
    [SerializeField]Sprite[] boxSprites;
    Box.Type boxType;
    Box.Type[] boxTypeSelection;
    int currentIndex = 0;
    
    private void Awake()
    {
        boxTypeSelection = new Box.Type[3];
        boxTypeSelection[0] = Box.Type.Bronze;
        boxTypeSelection[1] = Box.Type.Silver;
        boxTypeSelection[2] = Box.Type.Gold;
        UpdateCurrentBox();
    }

    public void ChangeBox(int change)
    {
        currentIndex = (currentIndex + change + boxTypeSelection.Length) % boxTypeSelection.Length;
        if (boxDisplay != null) boxDisplay.ChangeSprite(boxSprites[currentIndex]);
        UpdateCurrentBox();
    }

    public void ChangeBoxUp()
    {
        ChangeBox(1);
    }
    public void ChangeBoxDown()
    {
        ChangeBox(-1);
    }

    private void UpdateCurrentBox()
    {
        boxType = boxTypeSelection[currentIndex];
    }

    public Box.Type GetBoxType()
    {
        return boxType;
    }
}
