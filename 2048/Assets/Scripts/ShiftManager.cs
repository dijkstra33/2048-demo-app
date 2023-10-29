using System.Linq;
using UnityEngine;

namespace Game
{
    public class ShiftManager : MonoBehaviour
    {
        [SerializeField]
        private GridManager _gridManager;
        
        public void Shift(Vector2 direction)
        {
            GameManager.Instance.SetState(GameState.Moving);
            var orderedBlocks =
                _gridManager.Blocks
                    .OrderBy(b => direction.x > 0 ? b.Position.x : -b.Position.x)
                    .ThenBy(b => direction.y > 0 ? b.Position.y : -b.Position.y)
                    .ToArray();

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
                        }
                        if (possibleNextNode.occupiedBlock == null)
                        {
                            next = possibleNextNode;
                        }
                    }
                } while (next != block.node);

                block.transform.position = block.node.Position;
            }
            
            foreach (var block in orderedBlocks.Where(b => b.mergingBlock != null))
            {
                MergeBlocks(block, block.mergingBlock);
            }
            
            GameManager.Instance.SetState(GameState.SpawningBlocks);
        }

        private void MergeBlocks(Block baseBlock, Block mergingBlock)
        {
            _gridManager.SpawnBlock(baseBlock.node, baseBlock.Value * 2);
            _gridManager.RemoveBlock(baseBlock);
            _gridManager.RemoveBlock(mergingBlock);
        }

        private Node GetNodeAtPosition(Vector2 position)
        {
            return _gridManager.Nodes.FirstOrDefault(n => n.Position == position);
        }
    }
}