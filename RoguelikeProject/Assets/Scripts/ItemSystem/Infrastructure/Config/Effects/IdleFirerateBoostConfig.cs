using ItemSystem.Domain;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Idle Fire Rate Boost")]
    public class IdleFireRateBoostConfig : ItemEffectConfig
    {
        [field: SerializeField] public float FireRateBonusPerStack { get; private set; } = 0.2f;
        [field: SerializeField] public float IdleTimeRequired { get; private set; } = 2f;

        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeIdleFireRateBoost(this);
        }
    }
}