using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ShiftManager : MonoBehaviour
    {
        [SerializeField]
        private GridManager _gridManager;

        [SerializeField]
        private float _travelTime;
        
        public void Shift(Vector2 direction)
        {
            GameManager.Instance.SetState(GameState.Moving);
            var orderedBlocks =
                _gridManager.Blocks
                    .OrderBy(b => b.Position.x)
                    .ThenBy(b => b.Position.y)
                    .ToArray();

            if (direction == Vector2.right || direction == Vector2.up)
            {
                orderedBlocks = orderedBlocks.Reverse().ToArray();
            }

            foreach (var block in orderedBlocks)
            {
                var next = block.node;
                do
                {
                    block.SetBlock(next);
                    var possibleNextNode = GetNodeAtPosition(next.Position + direction);
                    if (possibleNextNode != null)
                    {
                        if (possibleNextNode.occupiedBlock != null &&
                            possibleNextNode.occupiedBlock.CanMerge(block.Value))
                        {
                            block.MergeBlock(possibleNextNode.occupiedBlock);
                        } else if (possibleNextNode.occupiedBlock == null)
                        {
                            next = possibleNextNode;
                        }
                    }
                } while (next != block.node);
            }

            var sequence = DOTween.Sequence();
            foreach (var block in orderedBlocks)
            {
                var movePoint = block.mergingBlock != null ? block.mergingBlock.Position : block.node.Position;
                sequence.Insert(0, block.transform.DOMove(movePoint, _travelTime));
            }

            sequence.OnComplete(() =>
            {
                foreach (var block in orderedBlocks.Where(b => b.mergingBlock != null))
                {
                    MergeBlocks(block, block.mergingBlock);
                }

                GameManager.Instance.SetState(GameState.SpawningBlocks);
            });
        }

        private void MergeBlocks(Block baseBlock, Block mergingBlock)
        {
            _gridManager.SpawnBlock(mergingBlock.node, baseBlock.Value * 2);
            _gridManager.RemoveBlock(baseBlock);
            _gridManager.RemoveBlock(mergingBlock);
        }

        private Node GetNodeAtPosition(Vector2 position)
        {
            return _gridManager.Nodes.FirstOrDefault(n => n.Position == position);
        }
    }
}