using System.Collections.Generic;
using UnityEngine;
using Weapons.Domain.Projectile.Interfaces;

namespace Weapons.Domain.Weapon.Interfaces
{
    public interface IWeapon
    {
        void Fire(Vector3 origin, Quaternion rotation, List<IProjectileModifier> modifiers);
        void Tick(float deltaTime, float fireRateMultiplier);
    }
}