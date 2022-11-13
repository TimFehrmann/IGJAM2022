using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ShopItem))]
public class DragDropShopItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private InputConfig inputConfig;

    private IInputProcessor input;
    // Cache
    private Shop shop;
    private ShopItem shopItem;

    private DraggedLevelItem itemBeingDragged;
    private float zIndex = 0;

    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
        shopItem = GetComponent<ShopItem>();
        input = inputConfig.InputType == InputConfig.INPUTTYPE.MOUSE ? new MouseInputProcessor() : new TouchInputProcessor();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create Duplicate to drag
        DraggedLevelItem levelItem = Instantiate(shopItem.DraggedLevelItem);
        levelItem.LevelItemPreview.SetPrice(shopItem.Price);
        itemBeingDragged = levelItem;

        zIndex += 0.01f;
        itemBeingDragged.transform.position += new Vector3(0,0,zIndex);
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

        bool inValidPlacementDueToIntersection = !itemBeingDragged.LevelItemPreview.IsValidPlacement();

        if (isInShopArea || cantAfford || inValidPlacementDueToIntersection)
        {

            Destroy(itemBeingDragged.gameObject);
        }
        else
        {
            shop.BuyItem(shopItem);
            itemBeingDragged.LevelItemPreview.SetPriceEnabled(false);
            shop.PlacementSystem.RegisterLevelItem(itemBeingDragged.LevelItem);
            itemBeingDragged.Place();
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
        itemBeingDragged.LevelItemPreview.SetCanBuy(canBuy);

        bool inValidPlacementDueToIntersection = !itemBeingDragged.LevelItemPreview.IsValidPlacement();
        // Display Delete Cross in Shop Area
        bool displayCross = !canBuy || isInShopArea || inValidPlacementDueToIntersection;
        itemBeingDragged.LevelItemPreview.SetCrossEnabled(displayCross);
    }

    private Vector2 GetMouseWorldPosition() {
        Vector2 inputPosition = input.InputPosition();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        return worldPosition;
    }
}
