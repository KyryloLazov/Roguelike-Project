using System;
using Player.Domain.Events;
using Player.Domain.Items.Player.Domain.Items;
using Player.Domain.PlayerStateMachine;
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
        private readonly PlayerEvents _playerEvents;
        private readonly PlayerStateMachineData _playerStateMachineData;
        private readonly CompositeDisposable _disposables = new();

        public PlayerStatusModel(IPlayerStatsProvider statsProvider, PlayerEvents playerEvents,
            PlayerStateMachineData playerStateMachineData)
        {
            _statsProvider = statsProvider;
            _playerEvents = playerEvents;
            _playerStateMachineData = playerStateMachineData;

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

        public void TakeDamage(float amount)
        {
            if (_playerStateMachineData.IsDashing.Value)
            {
                return;
            }
            
            int maxHealth = Mathf.CeilToInt(_statsProvider.GetStat(StatType.MaxHealth).Value);
            UnityEngine.Debug.Log($"Trying to changing Health from {_currentHealth.Value} to {_currentHealth.Value - amount}");
            _currentHealth.Value = Mathf.Clamp(_currentHealth.Value - amount, 0, maxHealth);
            _playerEvents.OnDamageTaken.OnNext(CurrentHealth.Value);
        }

        public void Heal(float amount)
        {
            TakeDamage(-amount);
            _playerEvents.OnDamageTaken.OnNext(CurrentHealth.Value);
        }

        public void Dispose() => _disposables.Dispose();
    }
}