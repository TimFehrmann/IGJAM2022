using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Vector2 ShopScale { get => scale; }

    public string Name;
    public int Price;
    public LevelItem levelItem;

    [SerializeField] private float scaleValue = 1;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;

    // Cache
    private Vector2 scale;

    private void Awake()
    {
        nameText.text = Name;
        costText.text = Price.ToString();
        scale = new Vector2(scaleValue, scaleValue);
    }
}
