using TMPro;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _goalText;
        
        [SerializeField]
        private GameObject _winScreen;
        
        [SerializeField]
        private GameObject _loseScreen;

        private void Start()
        {
            _goalText.text = $"Goal:\n{GameManager.Instance.WinCondition}";
        }

        public void ShowWinScreen()
        {
            _winScreen.SetActive(true);
        }
        
        public void ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
        }
    }
}