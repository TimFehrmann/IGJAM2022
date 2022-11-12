using UnityEngine;

public class ReflectionBehaviour : MonoBehaviour, IPlaceableBehaviour
{
    [Header("Local references")]
    [SerializeField]
    private Collider2D itemCollider;
    [SerializeField]
    private Transform itemtransform;
    [Header("Project references")]
    [SerializeField]
    private InputConfig inputConfig;

    private IInputProcessor input;

    private bool placementIsActive = false;

    private bool inRotationSelection = false;

    void Awake()
    {
        input = inputConfig.InputType == InputConfig.INPUTTYPE.MOUSE ? new MouseInputProcessor() : new TouchInputProcessor();
    }


    public void Despawn()
    {
        itemCollider.enabled = false;
        placementIsActive = false;
    }

    public void OnPlacementUpdate()
    {
        if (placementIsActive)
        {
            if (!inRotationSelection)
            {
                if (input.IsPressed())
                {
                    inRotationSelection = true;
                }
            }
            else
            {
                Vector2 inputPosition = input.InputPosition();
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
                itemtransform.LookAtPoint2D(worldPosition);
                if(!input.IsPressed())
                {
                    inRotationSelection = false;
                    placementIsActive = false;
                    itemCollider.enabled = true;
                }
            }

        }
    }

    public void Place()
    {
        placementIsActive = true;
    }

    public bool IsPlaced()
    {
        return itemCollider.enabled;
    }
}