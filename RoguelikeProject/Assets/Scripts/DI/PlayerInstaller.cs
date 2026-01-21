using Player.Domain.Input;
using Player.Domain.PlayerStateMachine;
using Player.Domain.PlayerStateMachine.Handlers;
using Player.Domain.PlayerStateMachine.States;
using Player.Domain.PlayerStats;
using Player.Infrastructure.Config;
using Player.Presentation;
using UnityEngine;

namespace DI
{
    public class PlayerInstaller : BaseBindings
    {
        [SerializeField] private PlayerValuesConfig _playerConfig;
        [SerializeField] private PlayerMover _playerMover;

        public override void InstallBindings()
        {
            BindInstance(_playerConfig);
            BindInstance(_playerMover);
            
            BindNewInstance<PlayerStatsModel>();
            BindNewInstance<PlayerStateMachineData>();
            BindNewInstance<InitializationPlayerStateMachine>();
            BindNewInstance<DashStateHandler>();
            BindNewInstance<MovementStateHandler>();
            BindNewInstance<IdleStateHandler>();
            BindNewInstance<StateHandleChain>();
            BindNewInstance<DesktopInputService>();
        }
    }
}