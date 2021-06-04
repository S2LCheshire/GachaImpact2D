
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TabButtonWithEffect : TabButton
{
    [SerializeField] CustomAction buttonEffect;
    [SerializeField] CustomAction buttonEffect2;
    [SerializeField] KeyCode clickButton = KeyCode.Return;
    [SerializeField] KeyCode clickButton2 = KeyCode.None;
    float clickTimer = 0;
    float clickTimerMax = 0.2f;

    private void Update()
    {
        if (clickTimer>0)clickTimer -= Time.deltaTime;
        if (Input.GetKey(clickButton) && isSelected) ClickWithDelay();
        else if (Input.GetKey(clickButton2) && isSelected) ClickWithDelay2();
    }

    public override void SelectTab()
    {
        base.SelectTab();
        //DOTween.KillAll();
        transform.DOScale(1.05f, 0.1f);
    }
    public override void UnselectTab()
    {
        base.UnselectTab();
        //DOTween.KillAll();
        transform.DOScale(1f, 0.1f);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        buttonEffect?.Invoke();
    }

    public void ClickWithDelay()
    {
        if (clickTimer <= 0)
        {
            buttonEffect?.Invoke();
            clickTimer = clickTimerMax;
        }
    }
    public void ClickWithDelay2()
    {
        if (clickTimer <= 0)
        {
            buttonEffect2?.Invoke();
            clickTimer = clickTimerMax;
        }
    }
}
