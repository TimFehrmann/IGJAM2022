using UnityEngine;

public class TouchInputProcessor : IInputProcessor
{
    public Vector2 InputPosition()
    {
        return Input.touches[0].position;
    }

    public bool IsPressed()
    {
        return Input.touchCount > 0;
    }
}