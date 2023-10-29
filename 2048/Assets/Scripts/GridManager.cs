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

        private List<Node> _nodes = new();
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        
        public void GenerateGrid()
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
            var freeNodes = _nodes.Where(n => n.occupiedBlock == null).ToArray();
            for (int i = 0; i < Math.Min(amount, freeNodes.Length); i++)
            {
                var freeNode = freeNodes[i];
                var block = Instantiate(_blockPrefab, freeNode.GetPosition(), Quaternion.identity, transform);
                var randomValue = UnityEngine.Random.Range(0f, 1f) > _fourSpawnChance ? 2 : 4;
                var blockType = GetBlockTypeByValue(randomValue);
                block.Init(blockType);
                freeNode.occupiedBlock = block;
            }

            if (freeNodes.Length <= amount)
            {
                GameManager.Instance.SetState(GameState.Lose);
            }
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