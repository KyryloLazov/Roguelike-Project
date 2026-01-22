using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/New Item")]
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: SerializeField] public List<ItemEffectConfig> Effects { get; private set; }
    }
}