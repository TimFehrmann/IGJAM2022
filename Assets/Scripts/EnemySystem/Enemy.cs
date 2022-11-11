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

    public Action<Enemy> OnDestruction;


    public void Initialize()
    {
        enemyRenderer.color = enemyColor;
    }

    public void UpdateMovement()
    {

    }

    public void Destroy()
    {
        if(OnDestruction != null)
        {
            OnDestruction(this);
        }
    }
}
