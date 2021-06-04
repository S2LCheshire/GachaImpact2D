using System;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    TextMeshProUGUI tmpro;
    [SerializeField] bool xPrefix = false;
    //[SerializeField] Action<int> subscribedEvent;

    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateText(int newInt)
    {
        tmpro.text = "";
        if (xPrefix) tmpro.text += "x";
        tmpro.text += newInt.ToString();
    }


}
