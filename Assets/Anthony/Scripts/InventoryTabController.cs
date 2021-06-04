using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTabController : TabController
{
    [SerializeField] int rowCount;
    [SerializeField] int columnCount;


    [SerializeField] KeyCode upButton = KeyCode.UpArrow;
    [SerializeField] KeyCode downButton = KeyCode.DownArrow;

    public SpriteDisplay spriteDisplay;
    protected override void MoveTabButtons()
    {
        if (Input.GetKeyDown(nextButton))
        {
            AddRightActiveTabIndex();
        }
        else if (Input.GetKeyDown(previousButton))
        {
            AddLeftActiveTabIndex();
        }
        else if (Input.GetKeyDown(downButton))
        {
            AddVerticalActiveTabIndex(columnCount);
        }
        else if (Input.GetKeyDown(upButton))
        {
            AddVerticalActiveTabIndex(-columnCount);
        }
    }

    private void AddRightActiveTabIndex()
    {

        int indexToAdd = 1;
        if ((activeTabIndex + 1) % columnCount == 0) indexToAdd = 1 - columnCount;
        ////Debug.Log("Active Tab Index : " + activeTabIndex);
        ////Debug.Log("index to add : " + indexToAdd);
        if (activeTabIndex + 1 + 1 > tabs.Length)
        {
            ////Debug.Log("changing index to : " + ((rowCount - 1) * columnCount));
            SetActiveTabIndex((rowCount - 1) * columnCount);
        }
        else
        {

           ////Debug.Log("changing index to : " + (activeTabIndex+indexToAdd));
           SetActiveTabIndex(activeTabIndex + indexToAdd);

        }
    }
    private void AddLeftActiveTabIndex()
    {

        int indexToAdd = -1;
        if (activeTabIndex % columnCount == 0) indexToAdd = columnCount - 1;
        if (activeTabIndex + 1 + indexToAdd > tabs.Length) SetActiveTabIndex(tabs.Length-1);
        else SetActiveTabIndex(activeTabIndex + indexToAdd);
    }
    private void AddVerticalActiveTabIndex(int indexToAdd)
    {
        int tabIndex = (activeTabIndex + indexToAdd + (rowCount * columnCount)) % (rowCount * columnCount);
        if (tabIndex + 1 > tabs.Length) tabIndex = tabs.Length - 1;
        SetActiveTabIndex(tabIndex);
    }
}
