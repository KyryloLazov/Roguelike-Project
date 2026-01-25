using Enemy.Presentation;
using UnityEngine;

namespace Enemy.Infrastructure.Config
{
    [CreateAssetMenu(menuName = "Configs/Enemies/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Prefab")]
        [field: SerializeField] public EnemyEntity Prefab { get; private set; }

        [Header("Base Stats")]
        [field: SerializeField] public float BaseHealth { get; private set; } = 100f;
        [field: SerializeField] public float BaseDamage { get; private set; } = 10f;
        [field: SerializeField] public float BaseSpeed { get; private set; } = 3f;

        [Header("Scaling")]
        [field: SerializeField] public EnemyScaling HealthScaling { get; private set; } = new();
        [field: SerializeField] public EnemyScaling DamageScaling { get; private set; } = new();
        [field: SerializeField] public EnemyScaling SpeedScaling { get; private set; } = new();
    }
}