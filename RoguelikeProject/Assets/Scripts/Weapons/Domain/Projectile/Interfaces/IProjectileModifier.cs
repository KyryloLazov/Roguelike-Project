using UnityEngine;
using Weapons.Presentation;

namespace Weapons.Domain.Projectile.Interfaces
{
    public interface IProjectileModifier
    {
        ProjectileData ModifyStats(ProjectileData stats) => stats;
        
        void OnHit(WeaponProjectile weaponProjectile, GameObject target, Vector3 point) { }
        
        void OnUpdate(WeaponProjectile weaponProjectile) { }
    }
}