using TMPro;
using UnityEngine;

public class SpriteDisplay : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    TextMeshProUGUI text;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateEmptyCollectibles();
    }
    private void ChangeSprite(Sprite sprite)
    {
        if (spriteRenderer != null) spriteRenderer.sprite = sprite;
    }
    private void ChangeAnimatorController(RuntimeAnimatorController animatorController)
    {
        if (animator!=null)animator.runtimeAnimatorController = animatorController;
    }
    private void ChangeText(string text)
    {
        this.text.text = text;
    }
    
    public void UpdateCollectibles(Collectibles collectibles)
    {
        ChangeSprite(collectibles.GetSprite());
        ChangeAnimatorController(collectibles.GetItemAnimatorController());
        string explanation = "Name: " + collectibles.GetItemName() + "\n\n";
        explanation += "Rarity: " + collectibles.GetRarity() + "\n\n";
        explanation += "Effect: " + collectibles.GetItemEffect();
        ChangeText(explanation);
    }
    public void UpdateEmptyCollectibles()
    {
        ChangeSprite(null);
        ChangeAnimatorController(null);
        string explanation = "Name: -\n\n";
        explanation += "Rarity: -\n\n";
        explanation += "Effect: -";
        ChangeText(explanation);
    }
    public void UpdateLockedCollectibles()
    {
        ChangeSprite(null);
        ChangeAnimatorController(null);
        string explanation = "Name: ???\n\n";
        explanation += "Rarity: ???\n\n";
        explanation += "Effect: ???";
        ChangeText(explanation);
    }
}
