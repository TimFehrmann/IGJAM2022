using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ShopItem))]
public class DragDropShopItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Cache
    private Shop shop;
    private ShopItem shopItem;

    private LevelItem itemBeingDragged;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
        shopItem = GetComponent<ShopItem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create Duplicate to drag
        LevelItem levelItem = Instantiate(shopItem.levelItem);
        levelItem.SetPrice(shopItem.Price);
        itemBeingDragged = levelItem;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!itemBeingDragged)
        {
            return;
        }

        DragShopItem();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check is in Shop Area
        Vector2 worldPosition = GetMouseWorldPosition();
        bool isInShopArea = shop.IsInShopArea(worldPosition);

        // Check can buy
        bool cantAfford = !shop.CheckCanBuy(shopItem);

        if (isInShopArea || cantAfford)
        {

            Destroy(itemBeingDragged.gameObject);
        }
        else
        {
            shop.BuyItem(shopItem);
            itemBeingDragged.SetPriceEnabled(false);
        }

        itemBeingDragged = null;
    }

    private void DragShopItem()
    {
        // Get Mouse Position
        Vector2 worldPosition = GetMouseWorldPosition();

        // Is in Shop Area
        bool isInShopArea = shop.IsInShopArea(worldPosition);

        // Update Scale
        // Make smaller in Shop Area and Scale to 1 outside ShopArea
        Vector2 itemToBeDraggedScale = isInShopArea ? shopItem.ShopScale : itemBeingDragged.LevelScale;
        itemBeingDragged.transform.localScale = itemToBeDraggedScale;

        // Update Position
        itemBeingDragged.transform.position = worldPosition;

        bool canBuy = shop.CheckCanBuy(shopItem);
        itemBeingDragged.SetCanBuy(canBuy);

        // Display Delete Cross in Shop Area
        bool displayCross = !canBuy || isInShopArea;
        itemBeingDragged.SetCrossEnabled(displayCross);
    }

    private Vector2 GetMouseWorldPosition() {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }
}
