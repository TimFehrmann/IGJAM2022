using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteRotator : MonoBehaviour
{
    private Dictionary<DIRECTION, Vector3> rotationMapClockwise = new Dictionary<DIRECTION, Vector3>();

    private const int ClockDirectionAngleDifference = 180;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();

        rotationMapClockwise.Add(DIRECTION.DOWN, new Vector3(0, 0, 90));
        rotationMapClockwise.Add(DIRECTION.UP, new Vector3(0, 0, 270));
        rotationMapClockwise.Add(DIRECTION.RIGHT, new Vector3(0, 0, 180));
        rotationMapClockwise.Add(DIRECTION.LEFT, new Vector3(0, 0, 0));
    }

    public void SetClockwiseDirection(DIRECTION direction)
    {
        int scale = direction.GetClockwiseDirection();
        myTransform.localScale = new Vector3(scale, 1, 1);
    }

    public void SetSpriteRotation(DIRECTION direction)
    {
        Vector3 clockwiseRotation = rotationMapClockwise[direction];

        if (Mathf.Sign(myTransform.localScale.x) > 0)
        {
            myTransform.rotation = Quaternion.Euler(clockwiseRotation);
        }
        else
        {
            float rotationZCounterClockwise = clockwiseRotation.z +
                ClockDirectionAngleDifference * direction.GetClockwiseDirection();
            myTransform.rotation = Quaternion.Euler(0, 0, rotationZCounterClockwise);
        }
    }
}
