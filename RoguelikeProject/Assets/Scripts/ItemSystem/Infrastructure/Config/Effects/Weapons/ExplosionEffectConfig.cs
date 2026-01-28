using ItemSystem.Domain;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/Effects/Weapons/Explosion Effect")]
    public class ExplosionEffectConfig : ItemEffectConfig
    {
        [field: SerializeField] public float Radius { get; private set; } = 5f;
        [field: SerializeField] public float Damage { get; private set; } = 7f;
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public string VFXEffect { get; private set; }
        [field: SerializeField] public string AudioKey { get; private set; }

        public override RuntimeItemEffect CreateRuntimeEffect()
        {
            return new RuntimeExplosionEffect(this);
        }
    }
}