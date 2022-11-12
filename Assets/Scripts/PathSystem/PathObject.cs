using System;
using UnityEngine;

public class PathObject : MonoBehaviour
{
    public Action OnNextPathObjectChanged;

    [SerializeField]
    private PathObject nextObject;

    [SerializeField]
    private PathObject previousObject;

    private Transform myTransform;

    public PathObject NextObject
    {
        get => nextObject;
        set
        {
            nextObject = value;
            OnNextPathObjectChanged();
        }
    }

    public PathObject PreviousObject { get => previousObject; set => previousObject = value; }

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public Vector2 GetPosition()
    {
        return myTransform.position;
    }
}
