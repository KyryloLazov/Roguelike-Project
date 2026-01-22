using UnityEngine;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Projectile.Effects
{
    public class ExplosionEffect : IProjectileModifier
    {
        private readonly float _radius;
        private readonly float _damage;

        public ExplosionEffect(float radius, float damage)
        {
            _radius = radius;
            _damage = damage;
        }
        
        public ProjectileData ModifyStats(ProjectileData stats)
        {
            stats.Speed *= 0.7f;
            stats.Lifetime += 1f;
            return stats;
        }

        public void OnHit(WeaponProjectile weaponProjectile, GameObject target, Vector3 point)
        {
           UnityEngine.Debug.Log("BOOM");
            // Physics.OverlapSphere(...)
        }
    }
}