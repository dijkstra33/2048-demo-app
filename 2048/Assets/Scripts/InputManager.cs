using UnityEngine;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private ShiftManager _shiftManager;
        
        public void TryToProcessInput()
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
    }
}