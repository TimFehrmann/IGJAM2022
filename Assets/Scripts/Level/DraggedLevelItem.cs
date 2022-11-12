using UnityEngine;

public class DraggedLevelItem : MonoBehaviour
{
    public LevelItemPreview LevelItemPreview;
    public LevelItem LevelItem;

    public Vector2 LevelScale { get => scale; }

    [SerializeField] private float scaleValue = 1;

    // Cache
    private Vector2 scale;

    public void Place()
    {
        // Release LevelItem
        LevelItem.transform.SetParent(null);
        // Delete Preview Stuff
        Destroy(gameObject);
    }

    private void Awake()
    {
        scale = new Vector2(scaleValue, scaleValue);
    }
}
