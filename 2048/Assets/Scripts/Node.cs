using System;
using UnityEngine;

namespace Game
{
    public class Node : MonoBehaviour
    {
        [NonSerialized]
        public Block occupiedBlock;

        public Vector2 Position => transform.position;
    }
}