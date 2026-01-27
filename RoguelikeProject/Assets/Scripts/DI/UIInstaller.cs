using UI.GameOver;
using UI.PlayerHUD;
using UnityEngine;
using Zenject;

namespace DI
{
    public class UIInstaller : BaseBindings
    {
        [Header("Scene Views")]
        [SerializeField] private PlayerHUDView _playerHUDView;
        [SerializeField] private GameOverView _gameOverView;

        public override void InstallBindings()
        {
            BindInstance(_playerHUDView);
            BindInstance(_gameOverView);
            
            BindNewInstance<PlayerHUDPresenter>();
            BindNewInstance<GameOverPresenter>();
        }
    }
}