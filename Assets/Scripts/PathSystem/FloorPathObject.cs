using UnityEngine;

public class FloorPathObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPathable objectOnPath = collision.gameObject.GetComponent<IPathable>();

        if (objectOnPath == null)
        {
            return;
        }

        objectOnPath.AddCollidingFloor(this);

        bool isFirstCollision = objectOnPath.IsFirstCollision();
        if (isFirstCollision)
        {
            return;
        }

        bool isClockwiseMovement = objectOnPath.GetIsClockwiseMovement();
        DIRECTION currentObjectDirection = objectOnPath.GetDirection();

        DIRECTION newDirection = isClockwiseMovement ?
            DirectionExtension.GetNextDirectionClockwise(currentObjectDirection) :
        DirectionExtension.GetNextDirectionCounterClockwise(currentObjectDirection);

        objectOnPath.SetMoveDirection(newDirection);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IPathable objectOnPath = collision.gameObject.GetComponent<IPathable>();

        if (objectOnPath == null)
        {
            return;
        }

         objectOnPath.RemoveCollidingFloor(this);
    }
}
