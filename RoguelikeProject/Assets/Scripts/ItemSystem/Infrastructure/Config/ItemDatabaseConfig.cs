using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Items/ItemDatabase")]
    public class ItemDatabaseConfig : ScriptableObject
    {
        [field: SerializeField] public List<ItemConfig> AllItems { get; private set; }
    }
}