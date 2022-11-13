using UnityEngine;

public class MouseInputProcessor : IInputProcessor
{
    public Vector2 InputPosition()
    {
        return Input.mousePosition;
    }

    public bool IsPressed()
    {
        return Input.GetMouseButton(0);
    }
}