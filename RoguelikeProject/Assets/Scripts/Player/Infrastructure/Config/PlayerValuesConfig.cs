using System.Collections.Generic;
using UnityEngine;

namespace Player.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "PlayerValuesConfig", menuName = "Configs/Player/PlayerValuesConfig")]
    public class PlayerValuesConfig : ScriptableObject
    {
        [Header("Start Value")]
        [field: SerializeField] public List<PlayerStat> PlayerStartStats { get; private set; }
    }
}
