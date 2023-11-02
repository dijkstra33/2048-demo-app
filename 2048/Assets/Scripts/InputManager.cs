using UnityEngine;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private ShiftManager _shiftManager;

        private void Update()
        {
            var gameState = GameManager.Instance.State;
            if (gameState == GameState.WaitingInput)
            {
                TryToProcessShiftInput();
                return;
            }

            if (gameState == GameState.Win | gameState == GameState.Lose)
            {
                TryToPrecessEndGameInput();
                return;
            }
        }
        
        private void TryToProcessShiftInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _shiftManager.Shift(Vector2.left);
            } 
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _shiftManager.Shift(Vector2.right);
            } 
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _shiftManager.Shift(Vector2.up);
            } 
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _shiftManager.Shift(Vector2.down);
            }
        }

        private void TryToPrecessEndGameInput()
        {
            if (Input.anyKeyDown)
            {
                GameManager.Instance.ReloadGame();
            }
        }
    }
}