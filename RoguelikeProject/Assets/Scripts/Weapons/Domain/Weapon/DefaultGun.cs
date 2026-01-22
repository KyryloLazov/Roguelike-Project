using System.Collections.Generic;
using UnityEngine;
using Weapons.Domain.Projectile;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Weapon
{
    public class DefaultGun : IWeapon
    {
        private readonly WeaponProjectile _weaponProjectilePrefab;
        private readonly float _fireRate;
        private readonly float _baseDamage;
        private readonly float _speed;

        private float _cooldownTimer;

        public DefaultGun(WeaponProjectile weaponProjectilePrefab, float fireRate, float damage, float speed)
        {
            _weaponProjectilePrefab = weaponProjectilePrefab;
            _fireRate = fireRate;
            _baseDamage = damage;
            _speed = speed;
        }

        public void Tick(float deltaTime)
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= deltaTime;
        }

        public void Fire(Vector3 origin, Quaternion rotation, List<IProjectileModifier> modifiers)
        {
            if (_cooldownTimer > 0) return;

            ProjectileData data = new ProjectileData
            {
                Damage = _baseDamage,
                Speed = _speed,
                Lifetime = 5f,
                Size = 1f
            };

            foreach (var modifier in modifiers)
            {
                data = modifier.ModifyStats(data);
            }

            WeaponProjectile weaponProjectile = Object.Instantiate(_weaponProjectilePrefab, origin, rotation);
            weaponProjectile.Initialize(data.Damage, data.Speed, data.Lifetime, modifiers);
            weaponProjectile.transform.localScale *= data.Size;

            _cooldownTimer = 1f / _fireRate;
        }
    }
}