using System;
using System.Collections;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] protected TabButton[] tabs;    
    [SerializeField] TabController[] previousTabs;
    [SerializeField] TabController[] nextTabs;

    protected KeyCode extraExitButton1 = KeyCode.Escape;
    protected KeyCode extraExitButton2 = KeyCode.Backspace;
    [SerializeField] protected KeyCode nextButton = KeyCode.RightArrow;
    [SerializeField] protected KeyCode previousButton = KeyCode.LeftArrow;
    [SerializeField] KeyCode activateNextTabsButton = KeyCode.UpArrow;
    [SerializeField] KeyCode activatePreviousTabsButton = KeyCode.DownArrow;

    [SerializeField] protected int activeTabIndex = 0;
    public bool isKeyActive = false;

    private void Awake()
    {
        tabs = GetComponentsInChildren<TabButton>();
    }

    private void OnEnable()
    {
        //tabs[activeTabIndex].SelectTab();
    }

    protected virtual void Update()
    {
        if (isKeyActive)
        {
            if (Input.GetKeyDown(extraExitButton1) || Input.GetKeyDown(extraExitButton2))
            {
                if (previousTabs[0] != null)
                {
                    StartCoroutine(previousTabs[0].ActivateKey());
                }
            }
            else if (!MoveBetweenTabs()) MoveTabButtons();
        }
    }
    private bool MoveBetweenTabs()
    {
        if (Input.GetKeyDown(activateNextTabsButton))
        {
            
            if (nextTabs[activeTabIndex] != null)
            {
                StartCoroutine(nextTabs[activeTabIndex].ActivateKey());
                //DeactivateKey();
                return true;
            }
            return false;
        }
        else if (Input.GetKeyDown(activatePreviousTabsButton))
        {
            if (previousTabs[activeTabIndex] != null)
            {
                StartCoroutine(previousTabs[activeTabIndex].ActivateKey());
                //DeactivateKey();
                return true;
            }
            return false;
        }
        else return false;
    }
    protected virtual void MoveTabButtons()
    {

        if (Input.GetKeyDown(nextButton))
        {
            AddActiveTabIndex(1);
        }
        else if (Input.GetKeyDown(previousButton))
        {
            AddActiveTabIndex(-1);
        }
    }

    public virtual void SetActiveTabIndex(TabButton activeTab)
    {
        tabs[activeTabIndex].UnselectTab();
        activeTabIndex = Array.IndexOf(tabs, activeTab);
        tabs[activeTabIndex].SelectTab();
    }

    protected void AddActiveTabIndex(int indexToAdd)
    {
        SetActiveTabIndex((activeTabIndex + indexToAdd + tabs.Length) % tabs.Length);
    }
    public virtual void SetActiveTabIndex(int indexToSet)
    {
        tabs[activeTabIndex].UnselectTab();
        activeTabIndex = indexToSet;
        tabs[activeTabIndex].SelectTab();
    }

    public IEnumerator ActivateKey()
    {

        foreach (TabController tabController in FindObjectsOfType<TabController>()) tabController.DeactivateKey(); 
        yield return new WaitForEndOfFrame();
        isKeyActive = true;
        SetActiveTabIndex(activeTabIndex);
        //AddActiveTabIndex(-1);
    }
    public void DeactivateKey()
    {
        isKeyActive = false;
        tabs[activeTabIndex].UnselectTab();
    }
    public TabButton GetCurrentButton()
    {
        return tabs[activeTabIndex];
    }

}
