using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;
        
        [SerializeField]
        private Node _nodePrefab;

        [SerializeField]
        private SpriteRenderer _boardPrefab;

        [SerializeField]
        private Block _blockPrefab;

        [SerializeField]
        private BlockType[] _blockTypes;

        [SerializeField]
        private float _fourSpawnChance;

        public List<Node> Nodes => _nodes;
        private readonly List<Node> _nodes = new();

        public List<Block> Blocks => _blocks;
        private readonly List<Block> _blocks = new();

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        
        public void GenerateLevel()
        {
            GenerateGrid();
            GameManager.Instance.SetState(GameState.SpawningBlocks);
        }
        
        private void GenerateGrid()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    var node = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity, transform);
                    _nodes.Add(node);
                }
            }

            var center = new Vector2((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f);
            var board = Instantiate(_boardPrefab, center, Quaternion.identity, transform);
            board.size = new Vector2(_width, _height);
            _mainCamera.transform.position = new Vector3(center.x, center.y, -10);
        }

        public void SpawnBlocks(int amount)
        {
            var randomFreeNodes = 
                _nodes
                    .Where(n => n.occupiedBlock == null)
                    .OrderBy(_ => UnityEngine.Random.value)
                    .ToArray();

            for (int i = 0; i < Math.Min(amount, randomFreeNodes.Length); i++)
            {
                var freeNode = randomFreeNodes[i];
                var randomValue = UnityEngine.Random.value > _fourSpawnChance ? 2 : 4;
                SpawnBlock(freeNode, randomValue);
            }

            if (randomFreeNodes.Length <= amount)
            {
                GameManager.Instance.SetState(GameState.Lose);
                return;
            }
            
            GameManager.Instance.SetState(GameState.WaitingInput);
        }

        public void SpawnBlock(Node node, int value)
        {
            var block = Instantiate(_blockPrefab, node.Position, Quaternion.identity, transform);
            var blockType = GetBlockTypeByValue(value);
            block.Init(blockType);
            block.node = node;
            node.occupiedBlock = block;
            _blocks.Add(block);
        }
        
        public void RemoveBlock(Block block)
        {
            _blocks.Remove(block);
            Destroy(block.gameObject);
        }

        private BlockType GetBlockTypeByValue(int value)
        {
            foreach (var blockType in _blockTypes)
            {
                if (blockType.Value == value)
                {
                    return blockType;
                }
            }

            return null;
        }
    }
}