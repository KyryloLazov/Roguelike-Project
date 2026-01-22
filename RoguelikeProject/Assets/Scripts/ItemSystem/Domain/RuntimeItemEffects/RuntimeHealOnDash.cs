using System;
using ItemSystem.Infrastructure.Config;
using UniRx;

namespace ItemSystem.Domain
{
    public class RuntimeHealOnDash : RuntimeItemEffect
    {
        private readonly HealOnDashConfig _config;
        private IDisposable _subscription;

        public RuntimeHealOnDash(HealOnDashConfig config)
        {
            _config = config;
        }

        public override void OnEquip(int stackCount)
        {
            _subscription = Context.Events.OnDashStarted
                .Subscribe(_ => Heal(stackCount));
        }

        public override void OnStackChanged(int newStackCount)
        {
            _subscription?.Dispose();
            _subscription = Context.Events.OnDashStarted
                .Subscribe(_ => Heal(newStackCount));
        }

        private void Heal(int stacks)
        {
            Context.Health.Heal(_config.HealAmountPerStack * stacks);
        }

        public override void OnUnequip()
        {
            _subscription?.Dispose();
        }

        public override void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}