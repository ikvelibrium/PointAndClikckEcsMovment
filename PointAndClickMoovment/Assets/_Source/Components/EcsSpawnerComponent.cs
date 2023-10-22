using System;
using UnityEngine;

namespace Client
{
    [Serializable]
    public struct EcsSpawnerComponent
    {
        public GameObject Prefab;
        public int Amount;
    }
}