using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GridManager _gridManager;

        private GameState _state;

        private void Awake()
        {
            Instance = this;
            SetState(GameState.GeneratingLevel);
        }

        public void SetState(GameState state)
        {
            _state = state;
            switch (state)
            {
                case GameState.GeneratingLevel:
                    _gridManager.GenerateGrid();
                    break;
                case GameState.SpawningBlocks:
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
