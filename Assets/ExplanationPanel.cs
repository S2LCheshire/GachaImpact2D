using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationPanel : MonoBehaviour
{
    Transform body;
    [SerializeField] TabController tabController;
    [SerializeField] KeyCode toggleButton = KeyCode.Return;
    [SerializeField] KeyCode toggleButton2 = KeyCode.Escape;
    bool isOn = false;
    private void Awake()
    {
        body = transform.Find("Body");
    }

    private void Start()
    {
        body.gameObject.SetActive(false);
        isOn = false;
    }

    private void Update()
    {
        if (isOn && (Input.GetKeyDown(toggleButton) || Input.GetKeyDown(toggleButton2))) TurnOff();
    }

    public void TurnOn()
    {
        ////Debug.Log("turning on");
        body.gameObject.SetActive(true);
        isOn = true;
        if (tabController!=null)tabController.DeactivateKey();
    }
    public void TurnOff()
    {
        ////Debug.Log("turning off");
        body.gameObject.SetActive(false);
        isOn = false;
        if (tabController != null) StartCoroutine(tabController.ActivateKey());
    }
}
