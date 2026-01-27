using UnityEngine;

namespace WaveControl.Infrastructure.Config
{
    [CreateAssetMenu(fileName = "WavesConfig", menuName = "Configs/Wave/WavesConfig")]
    public class WavesConfig : ScriptableObject
    {
        [field: SerializeField] public float BaseTimeBetweenSpawns { get; private set; } = 2f;
        [field: SerializeField] public float MinTimeBetweenWaves { get; private set; } = 0.5f;
        
        [field: SerializeField] public int BaseEnemiesToSpawns { get; private set; } = 5;
        [field: SerializeField] public int EnemiesWaveMultiplier { get; private set; } = 2;
        [field: SerializeField] public int ItemRewardCount { get; private set; } = 3;

        public float GetTimeBetweenWaves(int currentWave)
        {
            return Mathf.Max(MinTimeBetweenWaves, BaseTimeBetweenSpawns - (currentWave * 0.1f));
        }        
        
        public int GetEnemyCount(int currentWave)
        {
            return BaseEnemiesToSpawns + (currentWave * EnemiesWaveMultiplier);
        }
    }
}