using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private int defaultPrice;
    [SerializeField]
    private int priceIncreasePerPlacedItem;

    private int actualPrice;

    public Vector2 ShopScale { get => scale; }
    public int Price {
        get => actualPrice;
        set {
            actualPrice = value;
            OnPriceChanged();
        } 
    }

    public DraggedLevelItem DraggedLevelItem;

    [SerializeField] private float scaleValue = 1;
    [SerializeField] private TextMeshProUGUI costText;

    // Cache
    private Vector2 scale;

    private void Awake()
    {
        Price = defaultPrice;
        scale = new Vector2(scaleValue, scaleValue);
    }

    private void OnPriceChanged()
    {
        costText.text = Price.ToString();
    }

    public void UpdatePrice(int currentSpawnedAmount)
    {
        Price = defaultPrice + currentSpawnedAmount * priceIncreasePerPlacedItem;
    }
}
