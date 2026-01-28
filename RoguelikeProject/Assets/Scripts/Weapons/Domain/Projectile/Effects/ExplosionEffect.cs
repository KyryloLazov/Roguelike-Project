using Player.Domain.Items.Player.Domain.Items;
using UnityEngine;
using Weapons.Domain.Projectile.Interfaces;
using Weapons.Presentation;

namespace Weapons.Domain.Projectile.Effects
{
    public class ExplosionEffect : IProjectileModifier
    {
        private readonly float _radius;
        private readonly float _damage;
        private readonly string _vfxEffect;
        private readonly string _audioKey;
        private readonly LayerMask _layerMask;
        private readonly PlayerContext _context;

        public ExplosionEffect(float radius, float damage, string vfxEffect, string audioKey, LayerMask layerMask, 
            PlayerContext context)
        {
            _radius = radius;
            _damage = damage;
            _vfxEffect = vfxEffect;
            _audioKey = audioKey;
            _layerMask = layerMask;
            _context = context;
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
           
           Collider[] hits = Physics.OverlapSphere(point, _radius, _layerMask);

           foreach (var hit in hits)
           {
               if (hit.TryGetComponent(out IDamageable damageable))
               {
                   damageable.TakeDamage(_damage);
               }
           }
           
           if(_vfxEffect != "") _context.VFXManager.PlayEffect(_vfxEffect, point);
           if(_audioKey != "") _context.AudioHub.PlaySFX(_audioKey);
           UnityEngine.Debug.Log("BOOM!");
        }
    }
}