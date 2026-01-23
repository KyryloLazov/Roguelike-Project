using UnityEngine;
using Weapons.Domain.Weapon;
using Weapons.Domain.Weapon.Interfaces;
using Weapons.Presentation;

namespace Weapons.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapons/DefaultWeapon")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public WeaponProjectile WeaponProjectilePrefab { get; private set; }
        [field: SerializeField] public float BaseFireRate { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float BulletLifetime { get; private set; }

        public virtual IWeapon CreateWeapon()
        {
            return new DefaultGun(WeaponProjectilePrefab, BaseFireRate, Damage, Speed, BulletLifetime);
        }
    }
}