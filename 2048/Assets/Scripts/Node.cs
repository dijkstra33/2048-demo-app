using System;
using UnityEngine;

namespace Game
{
    public class Node : MonoBehaviour
    {
        [NonSerialized]
        public Block occupiedBlock;

        public Vector3 GetPosition() => transform.position;
    }
}