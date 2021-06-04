using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopUp : MonoBehaviour
{
    TextMeshProUGUI tmPro;
    float moveY = 2;
    float duration = 1;

    private void Awake()
    {
        tmPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(moveY, duration).SetRelative();
        tmPro.DOFade(0, duration);
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        tmPro.text = text;
    }

    public void SetColor(Color color)
    {
        tmPro.color = color;
    }

    public static PopUp Create(Vector3 position, string text, Color color)
    {
        GameObject textPopup = Instantiate(GameAssets.current.popUpPrefab, position, Quaternion.identity);
        textPopup.transform.SetParent(GameAssets.current.popUpCanvas.transform);
        PopUp popUp = textPopup.GetComponent<PopUp>();
        popUp.SetText(text);
        popUp.SetColor(color);
        return popUp;
    }
}
