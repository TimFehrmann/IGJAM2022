using UnityEngine;

public enum DIRECTION
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public static class DirectionExtension
{
    public static Vector2 ToVector2(this DIRECTION direction)
    {
        switch (direction)
        {
            case DIRECTION.UP:
                return Vector2.up;
            case DIRECTION.DOWN:
                return Vector2.down;
            case DIRECTION.LEFT:
                return Vector2.left;
            case DIRECTION.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    public static int GetClockwiseDirection(this DIRECTION direction)
    {
        switch (direction)
        {
            case DIRECTION.UP:
            case DIRECTION.RIGHT:
                return 1;
            case DIRECTION.DOWN:
            case DIRECTION.LEFT:
                return -1;
            default:
                return 0;
        }
    }
}