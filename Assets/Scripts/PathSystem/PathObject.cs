using System.Collections.Generic;
using UnityEngine;

public class PathObject : MonoBehaviour
{
    // Cached
    private Transform center;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPathable objectOnPath = collision.gameObject.GetComponent<IPathable>();

        if (objectOnPath == null)
        {
            return;
        }

        bool wasCollidingWithPathObject = objectOnPath.IsCollidingWithPathObject();

        objectOnPath.AddCollidingPathObject(this);

        bool ignoreFloorCollisionOnce = objectOnPath.CheckIgnoreFloorCollisionOnce(this);
        if (ignoreFloorCollisionOnce)
        {
            return;
        }


        // Moving onto this from previous Block -> No need to change direction
        if (wasCollidingWithPathObject)
        {
            // Avoid running into Walls though
            bool isFacingWall = ObjectOnPathIsFacingWall(objectOnPath);
            if (!isFacingWall)
            {
                return;
            }
        }

        bool isClockwiseMovement = objectOnPath.GetIsClockwiseMovement();
        DIRECTION currentObjectDirection = objectOnPath.GetDirection();

        DIRECTION newDirection = isClockwiseMovement ? 
            DirectionExtension.GetNextDirectionClockwise(currentObjectDirection) 
            : DirectionExtension.GetNextDirectionCounterClockwise(currentObjectDirection);
        objectOnPath.SetMoveDirection(newDirection);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPathable objectOnPath = collision.gameObject.GetComponent<IPathable>();

        if (objectOnPath == null)
        {
            return;
        }


        bool stillExists = objectOnPath.RemoveCollidingPathObject(this);
        if (!stillExists)
        {
            return;
        }

        // Continuing on next Block -> No need to change direction
        bool isCollidingWithPathObject = objectOnPath.IsCollidingWithPathObject();
        if (isCollidingWithPathObject)
        {
            Debug.Log("PathObject " + name + ": isCollidingWithPathObject!");
            return;
        }

        // Continuing on Floor -> floor changes direction
        bool isCollidingWithFloorObject = objectOnPath.IsCollidingWithFloor();
        if (isCollidingWithFloorObject)
        {
            Debug.Log("PathObject " + name + ": isCollidingWithFloorObject!");
            return;
        }     

        bool isClockwiseMovement = objectOnPath.GetIsClockwiseMovement();
        DIRECTION currentObjectDirection = objectOnPath.GetDirection();

        DIRECTION newDirection = isClockwiseMovement ? 
            DirectionExtension.GetNextDirectionCounterClockwise(currentObjectDirection) 
            : DirectionExtension.GetNextDirectionClockwise(currentObjectDirection);

        objectOnPath.SetMoveDirection(newDirection);

        Vector2 dirTowardsCenter = center.position - collision.transform.position;
        Vector2 offset = 0.1f * dirTowardsCenter.normalized;
        Debug.Log("PathObject offset: " + offset);

        objectOnPath.IgnoreNextTriggerEnterOfGivenPathObject(this);
        collision.transform.Translate(offset);
    }

    private void Awake()
    {
        center = transform.parent.GetComponent<Block>().Center;
    }

    private bool ObjectOnPathIsFacingWall(IPathable pathable)
    {
        DIRECTION direction = pathable.GetDirection();
        Vector2 moveDirection = DirectionExtension.ToVector2(direction);
        List<Transform> frontPositions = pathable.GetFrontTransforms();

        // heck at least one ray hits wall to face wall true
        foreach (Transform currentTransform in frontPositions)
        {
            Vector2 currentPosition = currentTransform.position;
            Debug.DrawRay(currentPosition, moveDirection, Color.blue, 100000);

            // Cast a ray straight downwards.
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, moveDirection);

            if (hit.collider != null)
            {
                float hitDistance = hit.distance;
                bool facingWall = hitDistance < 0.2f;
                if (facingWall)
                {
                    return true;
                }
            }

        }
        return false;
    }

}
