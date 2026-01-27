using System.Collections.Generic;
using Player.Domain.Input;
using Player.Domain.PlayerStats;
using Player.Infrastructure.Config;
using UnityEngine;
using Weapons.Domain.Pool;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Infrastructure.Config;
using Zenject;

namespace Weapons.Domain
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint; 
        
        private IInputService _inputService;
        private IPlayerStatsProvider _statsProvider;
        private List<IProjectileModifier> _modifiers = new();
        private WeaponProjectilePool _pool;
        private AudioHub _audioHub;

        private IWeapon _currentWeapon;
        
        [Inject]
        public void Construct(IInputService inputService, WeaponConfig defaultConfig, IPlayerStatsProvider statsProvider,
            WeaponProjectilePool pool, AudioHub audioHub)
        {
            _inputService = inputService;
            _pool = pool;
            _statsProvider = statsProvider;
            _currentWeapon = defaultConfig.CreateWeapon(_pool, _statsProvider);
            _audioHub = audioHub;
        }

        public void EquipWeapon(IWeapon newWeapon)
        {
            _currentWeapon = newWeapon;
        }        
        
        public void EquipWeapon(WeaponConfig config)
        {
            _currentWeapon = config.CreateWeapon(_pool, _statsProvider);
        }

        public void AddModifier(IProjectileModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Update()
        {
            if (_currentWeapon == null) return;
            
            float playerFireRateMultiplier = _statsProvider.GetStat(StatType.FireRate).Value;
            
            if (playerFireRateMultiplier <= 0) playerFireRateMultiplier = 1f;
            
            _currentWeapon.Tick(Time.deltaTime);

            if (_inputService.IsFireHeld)
            {
                _currentWeapon.Fire(_firePoint.position, _firePoint.rotation, _modifiers);
                _audioHub.PlaySFX("Fire");
            }
        }
    }
}