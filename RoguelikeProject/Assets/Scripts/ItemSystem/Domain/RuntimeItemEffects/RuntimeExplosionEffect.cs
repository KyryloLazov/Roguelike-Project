using ItemSystem.Infrastructure.Config;
using Weapons.Domain.Projectile.Effects;

namespace ItemSystem.Domain
{
    public class RuntimeExplosionEffect : RuntimeItemEffect
    {
        private ExplosionEffectConfig _config;

        public RuntimeExplosionEffect(ExplosionEffectConfig config)
        {
            _config = config;
        }
        
        public override void OnEquip(int stackCount)
        {
            Context.Weapons.AddModifier(new ExplosionEffect(_config.Radius, _config.Damage,
                _config.VFXEffect,
                _config.AudioKey,
                _config.LayerMask, Context));
        }

        public override void OnStackChanged(int newStackCount)
        { }

        public override void OnUnequip()
        { }
    }
}