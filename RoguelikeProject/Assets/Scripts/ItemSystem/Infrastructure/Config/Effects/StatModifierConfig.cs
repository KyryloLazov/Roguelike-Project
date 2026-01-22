using ItemSystem.Domain;
using Player.Infrastructure.Config;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Stat Modifier")]
    public class StatModifierConfig : ItemEffectConfig
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public float AmountPerStack { get; private set; }

        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeStatModifier(this);
        }
    }
}