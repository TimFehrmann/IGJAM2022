using UnityEngine;
using TMPro;

[RequireComponent(typeof(Income))]
public class Shop : MonoBehaviour
{
    [SerializeField] private Transform shopBorderLeft;
    [SerializeField] private TextMeshProUGUI cashText;

    // Cache
    private Income income;

    public bool IsInShopArea(Vector2 position)
    {
        return position.x > shopBorderLeft.position.x;
    }

    public void BuyItem(ShopItem shopItem)
    {
        income.Subtract(shopItem.Price);
    }

    private void Awake()
    {
        income = GetComponent<Income>();    
    }

    private void Update()
    {
        cashText.text =  "$ " + income.Cash.ToString();
    }
}
