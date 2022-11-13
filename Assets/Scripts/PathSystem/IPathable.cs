using System.Collections.Generic;
using UnityEngine;

public interface IPathable
{
    void SetMoveDirection(DIRECTION direction);
    bool GetIsClockwiseMovement();
    DIRECTION GetDirection();

    void AddCollidingFloor(FloorPathObject floorPath);
    void RemoveCollidingFloor(FloorPathObject floorPath);
    bool IsCollidingWithFloor();

    public List<Transform> GetFrontTransforms();

    bool IsFirstCollision();
    void AddCollidingPathObject(PathObject pathObject);
    void RemoveCollidingPathObject(PathObject pathObject, bool pathBeingDestroyed);
    bool IsCollidingWithPathObject();
    void IgnoreNextTriggerEnterOfGivenPathObject(PathObject pathObject);
    bool CheckIgnoreFloorCollisionOnce(PathObject pathObject);
    bool IfNotCollidingDestroy();
}
