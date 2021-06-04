using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] Panel[] panels;
    //int currentActivePanelIndex;

    private void Awake()
    {
        panels = new Panel[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            panels[i] = child.GetComponent<Panel>();
            i++;
        }
        //gameObject.SetActive(false);
    }

    private void Start()
    {
        //currentActivePanelIndex = 0;
        ActivatePanel(0);
    }

    public void ActivatePanel(int index)
    {
        ////Debug.Log("activating panel: "+ panels[index]);
        foreach (Panel panel in panels) panel.gameObject.SetActive(false);
        panels[index].gameObject.SetActive(true);
    }
    public void DectivateAllPanels()
    {

    }
}
