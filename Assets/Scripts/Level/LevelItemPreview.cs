using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class LevelItemPreview : MonoBehaviour
{

    [SerializeField] private SpriteRenderer crossSprite;
    [SerializeField] private TextMeshPro priceText;

    // Cache
    private Config config;
    private Collider2D collider2D;
    private List<PlacementBlocker> placementBlockers;

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
        placementBlockers = new List<PlacementBlocker>();

        config = FindObjectOfType<Config>();
        collider2D = GetComponent<Collider2D>();

        SetCrossEnabled(false);
    }

    public bool IsValidPlacement()
    {
        bool noIntersectingPlacementBlockers = placementBlockers.Count == 0;
        bool isValidPlacement = noIntersectingPlacementBlockers;
        return isValidPlacement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlacementBlocker placementBlocker = collision.GetComponent<PlacementBlocker>();

        if(placementBlocker == null)
        {
            return;
        }

        placementBlockers.Add(placementBlocker);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        PlacementBlocker placementBlocker = collision.GetComponent<PlacementBlocker>();

        if (placementBlocker == null)
        {
            return;
        }

        placementBlockers.Remove(placementBlocker);

    }

}
