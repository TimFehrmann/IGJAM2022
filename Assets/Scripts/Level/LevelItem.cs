using UnityEngine;

public class LevelItem : MonoBehaviour
{
    public Vector2 ShopScale { get => scale; }

    [SerializeField] private float scaleValue = 1;
    [SerializeField] private SpriteRenderer crossSprite;

    // Cache
    private Vector2 scale;


    public void SetCrossEnabled(bool enabled)
    {
        crossSprite.enabled = enabled;
    }


    private void Awake()
    {
        scale = new Vector2(scaleValue, scaleValue);
        SetCrossEnabled(false);
    }
}
