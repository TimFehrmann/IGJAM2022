using UnityEngine;

public interface IInputProcessor
{
    Vector2 InputPosition();

    bool IsPressed();
}