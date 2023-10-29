using TMPro;
using UnityEngine;

namespace Game
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _backgroundSpriteRenderer;
        
        [SerializeField]
        private TextMeshPro _valueText;

        public void Init(BlockType blockType)
        {
            _backgroundSpriteRenderer.color = blockType.Color;
            _valueText.text = blockType.Value.ToString();
        }
    }
}