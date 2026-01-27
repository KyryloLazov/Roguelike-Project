using Player.Domain.PlayerStats;
using UnityEngine;
using Weapons.Domain.Pool;
using Weapons.Domain.Weapon;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Presentation;

namespace Weapons.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "ShotgunConfig", menuName = "Configs/Weapons/Shotgun")]
    public class ShotgunConfig : WeaponConfig
    {
        [Header("Shotgun Specific")]
        [field: SerializeField] public int PelletCount { get; private set; } = 5;
        [field: SerializeField] public float SpreadAngle { get; private set; } = 30f;
        
        public override IWeapon CreateWeapon(WeaponProjectilePool pool, IPlayerStatsProvider stats)
        {
            return new Shotgun(WeaponProjectilePrefab, BaseFireRate, BaseDamage, 
                Speed, PelletCount, SpreadAngle, BulletLifetime, pool, stats);
        }
    }
}