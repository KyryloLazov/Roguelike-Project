using UnityEngine;
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
        
        public override IWeapon CreateWeapon()
        {
            return new Shotgun(WeaponProjectilePrefab, FireRate, Damage, 
                Speed, PelletCount, SpreadAngle, BulletLifetime);
        }
    }
}