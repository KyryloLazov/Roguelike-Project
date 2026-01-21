using Player.Infrastructure.Config;

namespace Player.Domain.PlayerStats
{
    public interface IPlayerStatsMutable : IPlayerStatsProvider
    {
        void ModifyStat(StatType type, float amount);
        void SetStat(StatType type, float value);
    }
}