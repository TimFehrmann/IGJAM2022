using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IPathable
{
    public Action<Enemy> OnDestruction;

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
    [SerializeField]
    private EnemySpriteRotator spriteRotator;

    private Transform myTransform;

    private Vector2 moveDirection;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
    }

    public void Initialize()
    {
        enemyRenderer.color = enemyColor;
    }

    public void UpdateMovement()
    {
        Vector2 frameMovement = moveDirection * speed * Time.deltaTime;
        myTransform.Translate(frameMovement);
    }

    public void Destroy()
    {
        if (OnDestruction != null)
        {
            OnDestruction(this);
        }
    }

    public void SetClockwiseDirection(DIRECTION direction)
    {
        spriteRotator.SetClockwiseDirection(direction);
    }

    public void SetMoveDirection(DIRECTION direction)
    {
        moveDirection = direction.ToVector2();
        spriteRotator.SetSpriteRotation(direction);
    }
}
