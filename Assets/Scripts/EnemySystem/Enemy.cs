using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private int hitpoints;
    [SerializeField]
    private Color enemyColor;

    [Header("Local References")]
    [SerializeField]
    private SpriteRenderer enemyRenderer;

    private PathObject nextPathObject;
    private Transform myTransform;

    public Action<Enemy> OnDestruction;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public void Initialize()
    {
        enemyRenderer.color = enemyColor;
    }

    public void UpdateMovement()
    {
        Vector2 targetPosition = nextPathObject.GetPosition();
        myTransform.position = Vector2.MoveTowards(myTransform.position, targetPosition, speed * Time.deltaTime);
        float distanceToTarget = Vector2.Distance(myTransform.position, targetPosition);
        if (distanceToTarget <= 1.0f)
        {
            nextPathObject = nextPathObject.NextObject;
        }
    }

    public void Destroy()
    {
        if(OnDestruction != null)
        {
            OnDestruction(this);
        }
    }
}
