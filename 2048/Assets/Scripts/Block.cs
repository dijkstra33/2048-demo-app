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

        public Vector3 Position => transform.position;

        public Node node;

        public bool merging;
        public Block mergingBlock;

        public int Value => _blockType.Value;
        private BlockType _blockType;

        public void Init(BlockType blockType)
        {
            _blockType = blockType;
            _backgroundSpriteRenderer.color = blockType.Color;
            _valueText.text = blockType.Value.ToString();
        }

        public void SetBlock(Node node)
        {
            if (this.node != null)
            {
                this.node.occupiedBlock = null;
            }

            this.node = node;
            this.node.occupiedBlock = this;
        }

        public void MergeBlock(Block blockToMergeWith)
        {
            mergingBlock = blockToMergeWith;
            node.occupiedBlock = null;
            blockToMergeWith.merging = true;
        }

        public bool CanMerge(int value) => Value == value && !merging && mergingBlock == null;
    }
}