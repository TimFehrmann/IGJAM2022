using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private ShopItem shopItem;

    private LevelItem itemBeingDragged;

    // Cache
    private Vector2 levelScale = Vector2.one;
    private Shop shop;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create Duplicate to drag
        LevelItem duplicate = Instantiate(shopItem.levelItem);
        itemBeingDragged = duplicate;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!itemBeingDragged)
        {
            return;
        }

        // Get Mouse Position
        Vector2 worldPosition = GetMouseWorldPosition();

        // Is in Shop Area
        bool isInShopArea = shop.IsInShopArea(worldPosition);

        // Update Scale
        // Make smaller in Shop Area and Scale to 1 outside ShopArea
        Vector2 itemToBeDraggedScale = isInShopArea ? shopItem.ShopScale : levelScale;
        itemBeingDragged.transform.localScale = itemToBeDraggedScale;

        // Display Delete Cross in Shop Area
        itemBeingDragged.SetCrossEnabled(isInShopArea);
        

        // Update Position
        itemBeingDragged.transform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check is in Shop Area
        Vector2 worldPosition = GetMouseWorldPosition();
        bool isInShopArea = shop.IsInShopArea(worldPosition);

        if (isInShopArea)
        {

            Destroy(itemBeingDragged.gameObject);
        }
        else
        {
            shop.BuyItem(shopItem);
        }

        itemBeingDragged = null;
    }

    private Vector2 GetMouseWorldPosition() {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }
}
