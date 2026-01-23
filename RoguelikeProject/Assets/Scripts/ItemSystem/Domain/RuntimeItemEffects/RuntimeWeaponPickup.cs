using ItemSystem.Infrastructure.Config;
using Weapons.Domain.Weapon.Interfaces;

namespace ItemSystem.Domain
{
    public class RuntimeWeaponPickup : RuntimeItemEffect
    {
        private WeaponPickupConfig _config;

        public RuntimeWeaponPickup(WeaponPickupConfig config)
        {
            _config = config;
        }
        
        public override void OnEquip(int stackCount)
        {
            Equip();
        }

        public override void OnStackChanged(int newStackCount)
        {
            Equip();
        }

        public override void OnUnequip() { }
        
        private void Equip()
        {
            IWeapon weapon = _config.WeaponConfig.CreateWeapon();
            Context.Weapons.EquipWeapon(weapon);
        }
    }
}