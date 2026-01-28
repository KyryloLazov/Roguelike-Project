using ItemSystem.Domain;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Weapons/Poison Effect")]
    public class PoisonEffectConfig : ItemEffectConfig
    {
        [field: SerializeField] public float DamagePerTick { get; private set; } = 5f;
        [field: SerializeField] public float Duration { get; private set; } = 7f;

        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimePoisonEffect(this);
        }
    }
}