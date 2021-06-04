using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextUpdate : MonoBehaviour
{
    TextMeshProUGUI tmpro;
    [SerializeField] PowerSelector powerSelector;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        tmpro.text = powerSelector.GetBreakPower().ToString();
    }
}