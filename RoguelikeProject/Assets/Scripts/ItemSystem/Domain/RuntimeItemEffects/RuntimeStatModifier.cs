using ItemSystem.Infrastructure.Config;

namespace ItemSystem.Domain
{
    public class RuntimeStatModifier : RuntimeItemEffect
    {
        private readonly StatModifierConfig _config;
        private float _appliedAmount;

        public RuntimeStatModifier(StatModifierConfig config)
        {
            _config = config;
        }

        public override void OnEquip(int stackCount)
        {
            Apply(stackCount);
        }

        public override void OnStackChanged(int newStackCount)
        {
            Context.Stats.ModifyStat(_config.StatType, -_appliedAmount);
            Apply(newStackCount);
        }

        public override void OnUnequip()
        {
            Context.Stats.ModifyStat(_config.StatType, -_appliedAmount);
            _appliedAmount = 0;
        }

        private void Apply(int stacks)
        {
            _appliedAmount = _config.AmountPerStack * stacks;
            Context.Stats.ModifyStat(_config.StatType, _appliedAmount);
        }
    }
}