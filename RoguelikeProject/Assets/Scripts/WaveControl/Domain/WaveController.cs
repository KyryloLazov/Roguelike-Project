using Enemy.Domain;
using Enemy.Infrastructure.Config;
using Enemy.Presentation;
using UnityEngine;
using WaveControl.Infrastructure.Config;
using Zenject;

namespace WaveControl.Domain
{
    public class WaveController : ITickable
    {
        private readonly EnemySpawner _spawner;
        private readonly EnemyConfig _config;
        private readonly WavesConfig _wavesConfig;
        private readonly Transform _playerTransform;

        private int _currentWave = 1;
        private float _timer;
        private float _timeBetweenSpawns;

        public WaveController(EnemySpawner spawner, EnemyConfig config,
            WavesConfig wavesConfig,
            [Inject(Id = "Player")] Transform playerTransform)
        {
            _spawner = spawner;
            _config = config;
            _wavesConfig = wavesConfig;
            _timeBetweenSpawns = wavesConfig.BaseTimeBetweenSpawns;
            _playerTransform = playerTransform;
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                SpawnEnemy();
                _timer = _timeBetweenSpawns;
            }
        }

        public void NextWave()
        {
            _currentWave++;
            _timeBetweenSpawns = _wavesConfig.GetTimeBetweenWaves(_currentWave);
            UnityEngine.Debug.Log($"Wave {_currentWave} started!");
        }

        private void SpawnEnemy()
        {
            var stats = new EnemyStats
            {
                Health = _config.BaseHealth * _config.HealthScaling.Evaluate(_currentWave),
                Damage = _config.BaseDamage * _config.DamageScaling.Evaluate(_currentWave),
                Speed  = _config.BaseSpeed  * _config.SpeedScaling.Evaluate(_currentWave),
            };
            
            stats.Health *= Random.Range(0.8f, 1.2f);
            stats.Speed *= Random.Range(0.8f, 1.2f);
            
            Vector3 spawnPos = GetRandomPointAroundPlayer();

            EnemyEntity enemy = _spawner.Spawn(stats, spawnPos);
            enemy.SetTarget(_playerTransform);
        }

        private Vector3 GetRandomPointAroundPlayer()
        {
            Vector2 randomCircle =Random.insideUnitCircle.normalized * 15f;
            Vector3 pos = _playerTransform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            return pos;
        }
    }
}