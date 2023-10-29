using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GridManager _gridManager;

        [SerializeField]
        private InputManager _inputManager;

        private GameState _state;
        private int _round = 0;

        private void Awake()
        {
            Instance = this;
            SetState(GameState.GeneratingLevel);
        }

        private void Update()
        {
            if (_state != GameState.WaitingInput)
            {
                return;
            }

            _inputManager.TryToProcessInput();
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
                    break;
                case GameState.Moving:
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
