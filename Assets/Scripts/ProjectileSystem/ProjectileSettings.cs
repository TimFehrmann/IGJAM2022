using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ProjectileSettings
{

    public float Speed;
    public int Damage;
    public float Size;
    public Color Color;
    public List<LayerMask> ReflectionLayer;

}