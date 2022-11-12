using UnityEngine;
using TMPro;

public class LevelItemPreview : MonoBehaviour
{

    [SerializeField] private SpriteRenderer crossSprite;
    [SerializeField] private TextMeshPro priceText;

    // Cache
    private Config config;

    public void SetPrice(int price)
    {
        priceText.text = price.ToString();
    }

    public void SetCrossEnabled(bool enabled)
    {
        crossSprite.enabled = enabled;
    }

    public void SetCanBuy(bool canBuy)
    {
        Color canBuyColor = canBuy ? config.TextValidColor : config.TextInvalidColor;
        priceText.color = canBuyColor;
    }

    public void SetPriceEnabled(bool enabled)
    {
        priceText.enabled = enabled;
    }

    private void Awake()
    {
        config = FindObjectOfType<Config>();
        SetCrossEnabled(false);
    }
}
