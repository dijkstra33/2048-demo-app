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
        
        [SerializeField]
        private GameObject _pressAnyKeyToRestratText;

        private void Start()
        {
            _goalText.text = $"Goal:\n{GameManager.Instance.WinCondition}";
        }

        public void ShowWinScreen()
        {
            _winScreen.SetActive(true);
            _pressAnyKeyToRestratText.SetActive(true);
        }
        
        public void ShowLoseScreen()
        {
            _loseScreen.SetActive(true);
            _pressAnyKeyToRestratText.SetActive(true);
        }
    }
}