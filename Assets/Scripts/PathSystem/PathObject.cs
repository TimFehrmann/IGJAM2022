using System;
using UnityEngine;

public class PathObject : MonoBehaviour
{
    [SerializeField]
    private DIRECTION pathDirectiion;

    public DIRECTION PathDirectiion { get => pathDirectiion; private set => pathDirectiion = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPathable objectOnPath = collision.gameObject.GetComponent<IPathable>();
        objectOnPath.SetMoveDirection(pathDirectiion);
    }
}
