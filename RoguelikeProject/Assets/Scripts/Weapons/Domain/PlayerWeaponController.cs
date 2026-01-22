using System.Collections.Generic;
using Player.Domain.Input;
using UnityEngine;
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
        private List<IProjectileModifier> _modifiers = new();

        private IWeapon _currentWeapon;
        
        [Inject]
        public void Construct(IInputService inputService, WeaponConfig defaultConfig)
        {
            _inputService = inputService;
            _currentWeapon = defaultConfig.CreateWeapon();
        }

        public void EquipWeapon(IWeapon newWeapon)
        {
            _currentWeapon = newWeapon;
        }

        public void AddModifier(IProjectileModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Update()
        {
            if (_currentWeapon == null) return;

            _currentWeapon.Tick(Time.deltaTime);

            if (_inputService.IsFireHeld)
            {
                _currentWeapon.Fire(_firePoint.position, _firePoint.rotation, _modifiers);
            }
        }
    }
}