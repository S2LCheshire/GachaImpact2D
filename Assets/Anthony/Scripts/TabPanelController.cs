using UnityEngine;

public class TabPanelController : TabController
{
    [SerializeField]PanelController panelController;

    public override void SetActiveTabIndex(TabButton activeTab)
    {
        base.SetActiveTabIndex(activeTab);
        panelController.ActivatePanel(activeTabIndex);
    }

    public override void SetActiveTabIndex (int indexToAdd)
    {
        base.SetActiveTabIndex(indexToAdd);
        panelController.ActivatePanel(activeTabIndex);
    }
}
