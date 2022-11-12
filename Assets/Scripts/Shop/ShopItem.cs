using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Vector2 ShopScale { get => scale; }

    public int Price;
    public DraggedLevelItem levelItem;

    [SerializeField] private float scaleValue = 1;
    [SerializeField] private TextMeshProUGUI costText;

    // Cache
    private Vector2 scale;

    private void Awake()
    {
        costText.text = Price.ToString();
        scale = new Vector2(scaleValue, scaleValue);
    }
}
