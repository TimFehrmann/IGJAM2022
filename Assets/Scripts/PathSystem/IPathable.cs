using System.Collections.Generic;
using UnityEngine;

public interface IPathable
{
    void SetMoveDirection(DIRECTION direction);
    bool GetIsClockwiseMovement();
    DIRECTION GetDirection();

    void AddCollidingFloor(FloorPathObject floorPath);
    bool RemoveCollidingFloor(FloorPathObject floorPath);
    bool IsCollidingWithFloor();

    public List<Transform> GetFrontTransforms();

    void AddCollidingPathObject(PathObject pathObject);
    bool RemoveCollidingPathObject(PathObject pathObject);
    bool IsCollidingWithPathObject();
    void IgnoreNextTriggerEnterOfGivenPathObject(PathObject pathObject);
    bool CheckIgnoreFloorCollisionOnce(PathObject pathObject);
}
