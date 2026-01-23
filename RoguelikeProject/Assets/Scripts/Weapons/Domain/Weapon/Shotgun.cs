using System.Collections.Generic;
using UnityEngine;
using Weapons.Domain.Projectile;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Weapon
{
    public class Shotgun : IWeapon
    {
        private readonly WeaponProjectile _weaponProjectilePrefab;
        private readonly float _fireRate;
        private readonly float _baseDamage;
        private readonly float _speed;
        private readonly float _lifetime;
        private readonly int _pelletCount;
        private readonly float _spreadAngle;

        private float _cooldownTimer;

        public Shotgun(WeaponProjectile weaponProjectilePrefab, float fireRate, float damage, 
            float speed, int pelletCount, float spreadAngle, float lifetime)
        {
            _weaponProjectilePrefab = weaponProjectilePrefab;
            _fireRate = fireRate;
            _baseDamage = damage;
            _speed = speed;
            _lifetime = lifetime;
            _pelletCount = pelletCount;
            _spreadAngle = spreadAngle;
        }

        public void Tick(float deltaTime, float fireRateMultiplier)
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= deltaTime * fireRateMultiplier;
        }

        public void Fire(Vector3 origin, Quaternion rotation, List<IProjectileModifier> modifiers)
        {
            if (_cooldownTimer > 0) return;
            
            ProjectileData baseData = new ProjectileData
            {
                Damage = _baseDamage,
                Speed = _speed,
                Lifetime = _lifetime,
                Size = 1f
            };

            foreach (var modifier in modifiers)
            {
                baseData = modifier.ModifyStats(baseData);
            }

            for (int i = 0; i < _pelletCount; i++)
            {
                float currentAngle = Random.Range(-_spreadAngle / 2f, _spreadAngle / 2f);
                
                Quaternion spreadRotation = rotation * Quaternion.Euler(0, currentAngle, 0);

                WeaponProjectile pellet = Object.Instantiate(_weaponProjectilePrefab, origin, spreadRotation);
                
                pellet.Initialize(baseData.Damage, baseData.Speed, baseData.Lifetime, modifiers);
                pellet.transform.localScale *= baseData.Size;
            }

            _cooldownTimer = 1f / _fireRate;
        }
    }
}