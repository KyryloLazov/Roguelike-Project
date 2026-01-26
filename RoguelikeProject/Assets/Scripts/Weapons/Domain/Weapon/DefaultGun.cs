using System.Collections.Generic;
using UnityEngine;
using Weapons.Domain.Pool;
using Weapons.Domain.Projectile;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Weapon
{
    public class DefaultGun : IWeapon
    {
        private readonly WeaponProjectile _weaponProjectilePrefab;
        private readonly WeaponProjectilePool _pool;
        private readonly float _fireRate;
        private readonly float _baseDamage;
        private readonly float _speed;
        private readonly float _lifetime;

        private float _cooldownTimer;
        
        public DefaultGun(WeaponProjectile weaponProjectilePrefab, float fireRate, float damage, float speed, 
            float lifetime, WeaponProjectilePool pool)
        {
            _weaponProjectilePrefab = weaponProjectilePrefab;
            _fireRate = fireRate;
            _baseDamage = damage;
            _speed = speed;
            _lifetime = lifetime;
            _pool = pool;
        }

        public void Tick(float deltaTime, float fireRateMultiplier)
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= deltaTime * fireRateMultiplier;
        }

        public void Fire(Vector3 origin, Quaternion rotation, List<IProjectileModifier> modifiers)
        {
            if (_cooldownTimer > 0) return;

            ProjectileData data = new ProjectileData
            {
                Damage = _baseDamage,
                Speed = _speed,
                Lifetime = _lifetime,
                Size = 1f
            };

            foreach (var modifier in modifiers)
            {
                data = modifier.ModifyStats(data);
            }

            WeaponProjectile weaponProjectile = _pool.Spawn(_weaponProjectilePrefab, origin, rotation);
            weaponProjectile.Initialize(data.Damage, data.Speed, data.Lifetime, modifiers);
            weaponProjectile.transform.localScale *= data.Size;

            _cooldownTimer = 1f / _fireRate;
        }
    }
}