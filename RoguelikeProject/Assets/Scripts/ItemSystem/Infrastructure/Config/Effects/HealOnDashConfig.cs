using ItemSystem.Domain;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Heal On Dash")]
    public class HealOnDashConfig : ItemEffectConfig
    {
        public int HealAmountPerStack;

        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeHealOnDash(this);
        }
    }
}