
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteChanger : MonoBehaviour
{
    Image image;
    Sprite lastSprite;
    bool isLocked = false;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeSprite(Sprite newSprite)
    {
        if (isLocked) return;
        lastSprite = image.sprite;
        image.sprite = newSprite;
    }

    public void UndoChangeSprite()
    {
        if (isLocked) return;
        Sprite holderSprite = image.sprite;
        image.sprite = lastSprite;
        lastSprite = holderSprite;
    }

    public void LockSpriteChanger()
    {
        isLocked = true;
    }
    public void UnlockSpriteChanger()
    {
        isLocked = false;
    }
}
