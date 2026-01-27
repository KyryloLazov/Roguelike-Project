using System;
using Player.Domain.PlayerStatus;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.GameOver
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOverView _view;
        private readonly PlayerStatusModel _playerStatus;
        private readonly CompositeDisposable _disposables = new();

        public GameOverPresenter(GameOverView view, PlayerStatusModel playerStatus)
        {
            _view = view;
            _playerStatus = playerStatus;
        }

        public void Initialize()
        {
            _view.OnRestartClicked += RestartGame;
            _view.OnQuitClicked += QuitGame;
            
            _playerStatus.CurrentHealth
                .Where(hp => hp <= 0)
                .Subscribe(_ => HandleDeath())
                .AddTo(_disposables);
        }

        private void HandleDeath()
        {
            Time.timeScale = 0;
            _view.Show();
        }

        private void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        public void Dispose()
        {
            _view.OnRestartClicked -= RestartGame;
            _view.OnQuitClicked -= QuitGame;
            _disposables.Dispose();
        }
    }
}