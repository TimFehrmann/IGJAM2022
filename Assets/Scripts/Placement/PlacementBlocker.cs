using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlacementBlocker : MonoBehaviour
{
    [SerializeField] private bool IsActiveFromStart = false;
    // Cached
    private Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = IsActiveFromStart;
    }

    public void Activate()
    {
        collider2D.enabled = true;
    }

    public void Deactivate()
    {
        collider2D.enabled = false;
    }
}
