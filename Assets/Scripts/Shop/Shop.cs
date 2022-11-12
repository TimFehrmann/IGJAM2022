using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform shopBorderLeft;
    [SerializeField] private Income income;

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