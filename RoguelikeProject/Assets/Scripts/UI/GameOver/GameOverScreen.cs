using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;

        public event Action OnRestartClicked;
        public event Action OnQuitClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
            _quitButton.onClick.AddListener(() => OnQuitClicked?.Invoke());
            _panel.SetActive(false);
        }

        public void Show()
        {
            _panel.SetActive(true);
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }
    }
}