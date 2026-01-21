using System;
using Player.Domain.PlayerStats;
using Player.Infrastructure.Config;
using UniRx;
using UnityEngine;

namespace Player.Domain.PlayerStatus
{
    public class PlayerStatusModel : IDisposable
    {
        public IReadOnlyReactiveProperty<float> CurrentHealth => _currentHealth;

        private readonly ReactiveProperty<float> _currentHealth;
        private readonly IPlayerStatsProvider _statsProvider;
        private readonly CompositeDisposable _disposables = new();

        public PlayerStatusModel(IPlayerStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;

            int maxHealth = Mathf.CeilToInt(_statsProvider.GetStat(StatType.MaxHealth).Value);
            _currentHealth = new ReactiveProperty<float>(maxHealth);

            _statsProvider.GetStat(StatType.MaxHealth)
                .Subscribe(newMax =>
                {
                    if (_currentHealth.Value > newMax)
                        _currentHealth.Value = newMax;
                })
                .AddTo(_disposables);
        }

        public void TakeDamage(int amount)
        {
            int maxHealth = Mathf.CeilToInt(_statsProvider.GetStat(StatType.MaxHealth).Value);
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - amount, 0, maxHealth);
        }

        public void Heal(int amount)
        {
            TakeDamage(-amount);
        }

        public void Dispose() => _disposables.Dispose();
    }
}