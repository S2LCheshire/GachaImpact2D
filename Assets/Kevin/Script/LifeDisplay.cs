using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField] Sprite lifeSpriteFull;
    [SerializeField] Sprite lifeSpriteEmpty;
    [SerializeField] int lifeIndex = 1;
    int currentMaxLife;
    Image heartImage;
    GameController gameController;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        gameController.lifeUpdate += CheckLife;
        InputController.GameStart += SetUpLife;

    }

    public void SetUpLife()
    {
        currentMaxLife = gameController.GetMaxLife();
        if (lifeIndex > currentMaxLife) heartImage.color = new Color(255, 255, 255, 0);
        else
        {
            heartImage.sprite = lifeSpriteFull;
            heartImage.color = new Color(255, 255, 255, 255);
        }
    }

    private void CheckLife(int currentLife)
    {
        if ((currentMaxLife+1-lifeIndex) > currentLife) heartImage.sprite = lifeSpriteEmpty;
    }

    private void LifeDown()
    {
        heartImage.sprite = lifeSpriteEmpty;
    }

    private void OnDestroy()
    {

        gameController.lifeUpdate -= CheckLife;
        InputController.GameStart -= SetUpLife;
    }

}
