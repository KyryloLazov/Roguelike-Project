using ItemSystem.Domain;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    public abstract class ItemEffectConfig : ScriptableObject
    {
        public abstract RuntimeItemEffect CreateRuntimeEffect();
    }
}