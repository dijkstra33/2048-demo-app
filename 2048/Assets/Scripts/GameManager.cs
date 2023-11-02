using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GridManager _gridManager;
        
        [SerializeField]
        private UIManager _uiManager;

        public int WinCondition => _winCondition;
        [SerializeField]
        private int _winCondition;

        public GameState State => _state;
        private GameState _state;
        private int _round = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetState(GameState.GeneratingLevel);
        }

        public void SetState(GameState state)
        {
            _state = state;
            switch (state)
            {
                case GameState.GeneratingLevel:
                    _gridManager.GenerateLevel();
                    break;
                case GameState.SpawningBlocks:
                    var blocksSpawnAmount = _round == 0 ? 2 : 1;
                    _gridManager.SpawnBlocks(blocksSpawnAmount);
                    _round++;
                    break;
                case GameState.WaitingInput:
                case GameState.Moving:
                    break;
                case GameState.CheckEndGameConditions:
                    CheckEndGameConditions();
                    break;
                case GameState.Win:
                    _uiManager.ShowWinScreen();
                    break;
                case GameState.Lose:
                    _uiManager.ShowLoseScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void CheckEndGameConditions()
        {
            if (_gridManager.Nodes.All(n => n.occupiedBlock != null))
            {
                SetState(GameState.Lose);
                return;
            }

            if (_gridManager.Blocks.Any(b => b.Value == _winCondition))
            {
                SetState(GameState.Win);
                return;
            }

            SetState(GameState.WaitingInput);
        }

        public void ReloadGame()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        }
    }
}
