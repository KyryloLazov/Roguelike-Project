using ItemSystem.Domain;
using Player.Infrastructure.Config;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Hp level boost")]
    public class HpLevelBoostConfig : ItemEffectConfig
    {
        [field: SerializeField, Range(0f, 1f)] public float HpLevel { get; private set; } = 0.2f;
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public float BoostPerStack { get; private set; } = 0.3f;
        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeHpLevelBoost(this);
        }
    }
}