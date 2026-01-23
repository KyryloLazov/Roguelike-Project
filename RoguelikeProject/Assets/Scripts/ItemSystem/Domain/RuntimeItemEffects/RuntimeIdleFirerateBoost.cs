using System;
using ItemSystem.Infrastructure.Config;
using Player.Domain.PlayerStats;
using Player.Infrastructure.Config;
using UniRx;
using UnityEngine;

namespace ItemSystem.Domain
{
public class RuntimeIdleFireRateBoost : RuntimeItemEffect
    {
        private readonly IdleFireRateBoostConfig _config;
        private readonly CompositeDisposable _disposables = new();
        private float _idleTimer;
        private bool _buffApplied;
        private float _totalBonus;

        public RuntimeIdleFireRateBoost(IdleFireRateBoostConfig config)
        {
            _config = config;
        }

        public override void OnEquip(int stackCount)
        {
            StartListening(stackCount);
        }

        public override void OnStackChanged(int newStackCount)
        {
            StopListening();
            StartListening(newStackCount);
        }

        public override void OnUnequip()
        {
            RemoveBuff();
            StopListening();
        }

        private void StartListening(int currentStack)
        {
            _idleTimer = 0f;
            _buffApplied = false;
            _totalBonus = _config.FireRateBonusPerStack * currentStack;

            Context.Events.OnIdleTick
                .Subscribe(delta =>
                {
                    _idleTimer += delta;

                    if (_idleTimer >= _config.IdleTimeRequired && !_buffApplied)
                    {
                        ApplyBuff();
                    }
                })
                .AddTo(_disposables);

            Context.StateData.DashRequested
                .Subscribe(_ => ResetIdle())
                .AddTo(_disposables);            
            
            Context.StateData.MovementInput
                .Subscribe(input =>
                {
                    if (input != Vector2.zero)
                    {
                        ResetIdle();
                    }
                })
                .AddTo(_disposables);
        }

        private void StopListening()
        {
            _disposables?.Dispose();
        }

        private void ResetIdle()
        {
            _idleTimer = 0;
            if (_buffApplied)
                RemoveBuff();
        }

        private void ApplyBuff()
        {
            _buffApplied = true;
            Context.Stats.ModifyStat(StatType.FireRate, _totalBonus);
        }

        private void RemoveBuff()
        {
            if (!_buffApplied) return;
            _buffApplied = false;
            Context.Stats.ModifyStat(StatType.FireRate, -_totalBonus);
        }
    }
}