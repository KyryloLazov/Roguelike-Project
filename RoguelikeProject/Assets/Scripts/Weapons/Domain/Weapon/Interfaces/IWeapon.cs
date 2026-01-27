using System.Collections.Generic;
using Player.Domain.PlayerStats;
using UnityEngine;
using Weapons.Domain.Pool;
using Weapons.Domain.Projectile.Interfaces;

namespace Weapons.Domain.Weapon.Interfaces
{
    public interface IWeapon
    {
        void Fire(Vector3 origin, Quaternion rotation, List<IProjectileModifier> modifiers);
        void Tick(float deltaTime);
    }
}