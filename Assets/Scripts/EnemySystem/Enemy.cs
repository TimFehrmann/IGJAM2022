using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPathable
{
    public event Action<Enemy> OnDestruction;

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

    [SerializeField] List<Transform> frontTransforms;

    private Transform myTransform;

    private DIRECTION direction;
    private Vector2 moveDirection;
    private List<FloorPathObject> collidingFloorPathObjects;
    private List<PathObject> collidingPathObjects;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        collidingFloorPathObjects = new List<FloorPathObject>();
        collidingPathObjects = new List<PathObject>();
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

    public bool GetIsClockwiseMovement()
    {
        return spriteRotator.transform.localScale.x > 0;
    }

    public DIRECTION GetDirection()
    {
        return direction;
    }


    private bool hasCollidedYet = false;
    public bool IsFirstCollision()
    {
        if (hasCollidedYet)
        {
            return false;
        }

        hasCollidedYet = true;
        return true;
    }

    public void AddCollidingFloor(FloorPathObject floorPath)
    {
        collidingFloorPathObjects.Add(floorPath);
    }

    public void RemoveCollidingFloor(FloorPathObject floorPath)
    {
        collidingFloorPathObjects.Remove(floorPath);

    }

    public bool IsCollidingWithFloor()
    {
        return collidingFloorPathObjects.Count > 0;
    }

    public void AddCollidingPathObject(PathObject pathObject)
    {
        collidingPathObjects.Add(pathObject);
    }

    public void RemoveCollidingPathObject(PathObject pathObject, bool pathBeingDestroyed)
    {
        collidingPathObjects.Remove(pathObject);
        if (pathBeingDestroyed)
        {
            Destroy();
        }
    }

    public bool IfNotCollidingDestroy()
    {
        bool isColliding = IsCollidingWithFloor() || IsCollidingWithPathObject();
        if (!isColliding)
        {
            Destroy();
            return true;
        }
        return false;
    }

    public bool IsCollidingWithPathObject()
    {
        return collidingPathObjects.Count > 0;
    }

    private PathObject pathObjectToIgnoreOnce;
    public void IgnoreNextTriggerEnterOfGivenPathObject(PathObject pathObject)
    {
        pathObjectToIgnoreOnce = pathObject;
    }
    public bool CheckIgnoreFloorCollisionOnce(PathObject pathObject)
    {
        bool ignoreOnce = pathObjectToIgnoreOnce == pathObject;
        pathObjectToIgnoreOnce = null;
        return ignoreOnce;
    }

    public void Destroy()
    {
        if (OnDestruction != null)
        {
            pathObjectToIgnoreOnce = null;
            OnDestruction(this);
        }
    }

    public void SetClockwiseDirection(DIRECTION direction)
    {
        spriteRotator.SetClockwiseDirection(direction);
    }

    public void SetMoveDirection(DIRECTION directionParam)
    {
        direction = directionParam;
        moveDirection = direction.ToVector2();
        spriteRotator.SetSpriteRotation(direction);
    }

    public List<Transform> GetFrontTransforms()
    {
        return frontTransforms;
    }
}
