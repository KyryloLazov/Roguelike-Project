using System;
using System.Collections.Generic;
using Player.Infrastructure.Config;
using UniRx;

namespace Player.Domain.PlayerStats
{
    public class PlayerStatsModel : IPlayerStatsMutable, IDisposable
    {
        private readonly Dictionary<StatType, ReactiveProperty<float>> _stats = new();
        private readonly CompositeDisposable _disposables = new();

        public PlayerStatsModel(PlayerValuesConfig config)
        {
            foreach (var statData in config.PlayerStartStats)
            {
                if (!_stats.ContainsKey(statData.Type))
                {
                    var reactiveStat = new ReactiveProperty<float>(statData.Value);
                    _stats.Add(statData.Type, reactiveStat);
                    
                    reactiveStat.AddTo(_disposables);
                }
            }
        }

        public IReadOnlyReactiveProperty<float> GetStat(StatType type)
        {
            if (_stats.TryGetValue(type, out var stat))
            {
                return stat;
            }
            
            return new ReactiveProperty<float>(0); 
        }

        public void ModifyStat(StatType type, float amount)
        {
            if (_stats.TryGetValue(type, out var stat))
            {
                stat.Value += amount;
                UnityEngine.Debug.Log($"Adding {amount} to {type}");
            }
        }

        public void SetStat(StatType type, float value)
        {
            if (_stats.TryGetValue(type, out var stat))
            {
                stat.Value = value;
                UnityEngine.Debug.Log($"Setting {type} to {value}");
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}