using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class BlockType
    {
        public int Value => _value;
        [SerializeField]
        private int _value;

        public Color Color => _color;
        [SerializeField]
        private Color _color;
    }
}