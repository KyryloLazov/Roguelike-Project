using ItemSystem.Infrastructure.Config;
using UnityEngine;
using Weapons.Domain.Projectile.Effects;

namespace ItemSystem.Domain
{
    public class RuntimePoisonEffect : RuntimeItemEffect
    {
        private PoisonEffectConfig _config;

        public RuntimePoisonEffect(PoisonEffectConfig config)
        {
            _config = config;
        }
        
        public override void OnEquip(int stackCount)
        {
            Context.Weapons.AddModifier(new PoisonEffect(_config.DamagePerTick, _config.Duration));
        }

        public override void OnStackChanged(int newStackCount)
        { }

        public override void OnUnequip()
        { }
    }
}