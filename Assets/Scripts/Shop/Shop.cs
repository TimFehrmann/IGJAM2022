using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public PlacementSystem PlacementSystem { get => placementSystem; private set => placementSystem = value; }

    [SerializeField] private Transform shopBorderLeft;
    [SerializeField] private Income income;
    // Cached
    private PlacementSystem placementSystem;


    private void Awake()
    {
        PlacementSystem = FindObjectOfType<PlacementSystem>();
    }

    public bool IsInShopArea(Vector2 position)
    {
        return position.y < shopBorderLeft.position.y;
    }

    public void BuyItem(ShopItem shopItem)
    {
        income.Subtract(shopItem.Price);
    }

    public bool CheckCanBuy(ShopItem shopItem)
    {
        return shopItem.Price <= income.Cash;
    }
}