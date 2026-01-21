using Player.Infrastructure.Config;
using UniRx;

namespace Player.Domain.PlayerStats
{
    public interface IPlayerStatsProvider
    {
        IReadOnlyReactiveProperty<float> GetStat(StatType type);
    }
}