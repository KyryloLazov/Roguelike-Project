using ItemSystem.Domain;
using UnityEngine;
using Weapons.Infrastructure.Config;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Weapon Pickup")]
    public class WeaponPickupConfig: ItemEffectConfig
    {
        [field: SerializeField] public WeaponConfig WeaponConfig;
        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeWeaponPickup(this);
        }
    }
}