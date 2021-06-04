using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DisplayMoveController : MonoBehaviour
{
    TextMeshProUGUI displayText;
    float moveHorizontalX;
    float moveVerticalY;
    [SerializeField] int moveHorizontally;
    [SerializeField] int moveVertically;
    [SerializeField] int flipHorizontally;
    [SerializeField] int flipVertically;
    bool isMoveHorizontal;
    bool isMoveVertical;
    bool isFlipHorizontal;
    bool isFlipVertical;

    readonly System.Random rnd = new System.Random();
    public static DisplayMoveController current;


    private void Awake()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        isMoveHorizontal = false;
        isMoveVertical = false;
        isFlipHorizontal = false;
        isFlipVertical = false;

    }

    
    // Update is called once per frame
    void Update()
    {

    }

    public void MoveHorizontal(float x, float duration)
    {
        transform.DOMoveX(x, duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void MoveVertical(float y, float duration)
    {
        transform.DOMoveY(y, duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void FlipHorizontal(float duration)
    {
        if (transform.localScale.x < 0) transform.DOScaleX(1, duration).SetLoops(-1, LoopType.Yoyo);
        else transform.DOScaleX(-1, duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void FlipVertical(float duration)
    {
        if (transform.localScale.y < 0) transform.DOScaleY(1, duration).SetLoops(-1, LoopType.Yoyo);
        else transform.DOScaleY(-1, duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetText(string text, float currentTimer, int currentScore = 0)
    {
        displayText.text = text;

        if(currentScore >= (moveHorizontally.Equals(0) ? 5 : moveHorizontally))
        {
            if (moveHorizontalX >= 0) moveHorizontalX = rnd.Next(-82, 0)/10;
            else moveHorizontalX = rnd.Next(0, 82)/10;

            isMoveHorizontal = true;

            MoveHorizontal(moveHorizontalX, currentTimer/1.5f);
        }

        if(currentScore >= (moveVertically.Equals(0) ? 10 : moveVertically))
        {
            if (moveVerticalY >= 0) moveVerticalY = rnd.Next(-48,0)/10;
            else moveVerticalY = rnd.Next(0, 48)/10;

            isMoveVertical = true;

            MoveVertical(moveVerticalY, currentTimer / 1.5f);
        }

        if (currentScore >= (flipHorizontally.Equals(0) ? 15 : flipHorizontally))
        {
            FlipHorizontal(Math.Max(currentTimer, 0.8f));
            isFlipHorizontal = true;
        }

        if (currentScore >= (flipVertically.Equals(0) ? 20 : flipVertically))
        {
            FlipVertical(Math.Max(currentTimer, 0.8f));
            isFlipVertical = true;
        }
    }

    public string GetText()
    {
        return displayText.text;
    }

    public float CountModifier()
    {
        int x = 0;
        int x1 = isMoveHorizontal ? 1 : 0;
        int x2 = isMoveVertical ? 1 : 0;
        int x3 = isFlipHorizontal ? 1 : 0;
        int x4 = isFlipVertical ? 1 : 0;
        x = x + x1 + x2 + x3 + x4;
        return x;
    }


}
