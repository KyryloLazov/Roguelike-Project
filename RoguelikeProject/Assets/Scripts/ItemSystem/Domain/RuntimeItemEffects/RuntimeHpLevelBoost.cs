using System;
using ItemSystem.Infrastructure.Config;
using Player.Infrastructure.Config;
using UniRx;
using UnityEngine;

namespace ItemSystem.Domain
{
    public class RuntimeHpLevelBoost : RuntimeItemEffect
    {
        private readonly HpLevelBoostConfig _config;
        private readonly CompositeDisposable _disposables = new();

        private float _totalBonus;
        private bool _buffApplied;

        public RuntimeHpLevelBoost(HpLevelBoostConfig config)
        {
            _config = config;
        }

        public override void OnEquip(int stackCount)
        {
            _totalBonus = _config.BoostPerStack * stackCount;
            StartListening();
        }

        public override void OnStackChanged(int newStackCount)
        {
            _totalBonus = _config.BoostPerStack * newStackCount;
        }

        public override void OnUnequip()
        {
            RemoveBuff();
            StopListening();
        }

        private void StartListening()
        {
            StopListening();
            
            Context.Health.CurrentHealth
                .CombineLatest(Context.Stats.GetStat(StatType.MaxHealth), (current, max) =>
                {
                    if (max <= 0) return 1f;
                    return current / max;
                })
                .Subscribe(ratio =>
                {
                    if (!_buffApplied && ratio <= _config.HpLevel)
                    {
                        ApplyBuff();
                    }
                    else if (_buffApplied && ratio > _config.HpLevel)
                    {
                        RemoveBuff();
                    }
                })
                .AddTo(_disposables);
        }

        private void StopListening()
        {
            _disposables.Clear();
        }

        private void ApplyBuff()
        {
            _buffApplied = true;
            Context.Stats.ModifyStat(_config.StatType, _totalBonus);
            UnityEngine.Debug.Log($"[HpLevelBoost] Applied bonus {_totalBonus} to {_config.StatType}");
        }

        private void RemoveBuff()
        {
            if (!_buffApplied) return;
            _buffApplied = false;
            Context.Stats.ModifyStat(_config.StatType, -_totalBonus);
            UnityEngine.Debug.Log($"[HpLevelBoost] Removed bonus {_totalBonus} from {_config.StatType}");
        }
    }
}