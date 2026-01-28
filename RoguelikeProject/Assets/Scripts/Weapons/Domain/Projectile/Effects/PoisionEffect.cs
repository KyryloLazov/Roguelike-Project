using StatusEffects.Domain;
using StatusEffects.Presentation;
using UnityEngine;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Projectile.Effects
{
    public class PoisonEffect : IProjectileModifier
    {
        private readonly float _damagePerSecond;
        private readonly float _duration;

        public PoisonEffect(float damagePerSecond, float duration)
        {
            _damagePerSecond = damagePerSecond;
            _duration = duration;
        }
        
        public ProjectileData ModifyStats(ProjectileData stats)
        {
            stats.Damage *= 0.8f; 
            return stats;
        }

        public void OnHit(WeaponProjectile weaponProjectile, GameObject target, Vector3 point)
        {
            if (target.TryGetComponent(out StatusEffectReceiver receiver))
            {
                receiver.ApplyEffect(new PoisonStatusEffect(_damagePerSecond, _duration));
            }
            
            UnityEngine.Debug.Log($"Poison applied to {target.name} for {_duration}s");
        }
    }
}