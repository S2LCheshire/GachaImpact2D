using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    PanelController panelController;
    private void Awake()
    {
        panelController = GetComponentInParent<PanelController>();
    }
}
