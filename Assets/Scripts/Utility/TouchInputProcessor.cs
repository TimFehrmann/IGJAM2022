using UnityEngine;

public class TouchInputProcessor : IInputProcessor
{
    public Vector2 InputPosition()
    {
        return Input.touches[0].position;
    }
}