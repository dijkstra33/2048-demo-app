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
        private GameObject _gridCellPrefab;

        [SerializeField]
        private SpriteRenderer _boardPrefab;

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
                    Instantiate(_gridCellPrefab, new Vector2(x, y), Quaternion.identity, transform);
                }
            }

            var center = new Vector2((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f);
            var board = Instantiate(_boardPrefab, center, Quaternion.identity);
            board.size = new Vector2(_width, _height);
            _mainCamera.transform.position = new Vector3(center.x, center.y, -10);
        }
    }
}