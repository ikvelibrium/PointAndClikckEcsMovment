using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client 
{   [Serializable]
    public struct EcsMooverComponent 
    {
        public InputAction MouseClick;
        public float Speed;
        public Transform SoldierPosition;
        public Camera Camera;
    }
}