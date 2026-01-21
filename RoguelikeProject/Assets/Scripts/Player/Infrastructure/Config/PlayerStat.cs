using System;

namespace Player.Infrastructure.Config
{
    [Serializable]
    public class PlayerStat
    {
        public StatType Type;
        public float Value;
    }
}