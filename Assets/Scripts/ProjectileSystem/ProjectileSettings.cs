using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ProjectileSystem
{
    [Serializable]
    public struct ProjectileSettings 
    {
        
        public float Speed;
        public int Damage;
        public float Size;
        public Color Color;
        public List<LayerMask> ReflectionLayer;
        
    }
}