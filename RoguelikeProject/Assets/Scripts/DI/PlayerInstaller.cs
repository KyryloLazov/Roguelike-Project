using Gameflow.Domain;
using Gameflow.Presentation;
using ItemSystem.Domain;
using ItemSystem.Infrastructure.Config;
using Player.Domain.Events;
using Player.Domain.Input;
using Player.Domain.Items.Player.Domain.Items;
using Player.Domain.PlayerStateMachine;
using Player.Domain.PlayerStateMachine.Handlers;
using Player.Domain.PlayerStateMachine.States;
using Player.Domain.PlayerStats;
using Player.Domain.PlayerStatus;
using Player.Infrastructure.Config;
using Player.Presentation;
using UnityEngine;
using Weapons.Domain;
using Weapons.Domain.Pool;
using Weapons.Infrastructure.Config;

namespace DI
{
    public class PlayerInstaller : BaseBindings
    {
        [SerializeField] private PlayerValuesConfig _playerConfig;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private WeaponConfig _startWeaponConfig;
        [SerializeField] private PlayerWeaponController _playerWeaponController;
        [SerializeField] private ItemDatabaseConfig _itemDatabase;
        [SerializeField] private RewardUI _rewardUI;

        public override void InstallBindings()
        {
            BindInstance(_playerConfig);
            BindInstance(_playerMover);
            
            BindNewInstance<DesktopInputService>();
            
            BindNewInstance<PlayerStatsModel>();
            BindNewInstance<PlayerStatusModel>();
            
            BindNewInstance<PlayerStateMachineData>();
            BindNewInstance<InitializationPlayerStateMachine>();
            BindNewInstance<DashStateHandler>();
            BindNewInstance<MovementStateHandler>();
            BindNewInstance<IdleStateHandler>();
            BindNewInstance<StateHandleChain>();
            
            Container.BindInstance(_startWeaponConfig);
            Container.BindInstance(_playerWeaponController);
            BindNewInstance<WeaponProjectilePool>();
            
            BindNewInstance<PlayerEvents>();
            BindNewInstance<PlayerContext>();
            BindNewInstance<InventoryController>();
            
            Container.BindInstance(_itemDatabase);
            Container.Bind<RewardService>().AsSingle();
            
            Container.BindInstance(_rewardUI);
             
            Container.BindInterfacesAndSelfTo<GamePhaseService>().AsSingle().NonLazy();
        }
    }
}