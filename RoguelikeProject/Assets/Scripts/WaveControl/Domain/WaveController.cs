using System;
using Enemy.Domain;
using Enemy.Infrastructure.Config;
using Enemy.Presentation;
using UnityEngine;
using WaveControl.Infrastructure.Config;
using Zenject;
using Random = UnityEngine.Random;

namespace WaveControl.Domain
{
    public class WaveController : ITickable
    {
        private readonly EnemySpawner _spawner;
        private readonly EnemyConfig _config;
        private readonly WavesConfig _wavesConfig;
        private readonly Transform _playerTransform;
        
        public event Action OnWaveCompleted;

        private int _currentWave;
        private float _spawnTimer;
        private float _timeBetweenSpawns;
        
        private int _enemiesToSpawn;
        private int _enemiesSpawned;
        private int _enemiesAlive;
        
        private bool _isWaveActive = false;

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
        
        public void StartNextWave()
        {
            _currentWave++;
            _enemiesSpawned = 0;
            _enemiesAlive = 0;
            _isWaveActive = true;
            
            _enemiesToSpawn = _wavesConfig.GetEnemyCount(_currentWave);
            _timeBetweenSpawns = _wavesConfig.GetTimeBetweenWaves(_currentWave);
            
            UnityEngine.Debug.Log($"Wave {_currentWave} STARTED! Enemies: {_enemiesToSpawn}");
        }

        public void Tick()
        {
            if (!_isWaveActive) return;
            
            if (_enemiesSpawned < _enemiesToSpawn)
            {
                _spawnTimer -= Time.deltaTime;
                if (_spawnTimer <= 0)
                {
                    SpawnEnemy();
                    _spawnTimer = _timeBetweenSpawns;
                }
            }
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
            
            enemy.OnDeath += HandleEnemyDeath;
            
            _enemiesSpawned++;
            _enemiesAlive++;
        }
        
        private void HandleEnemyDeath(EnemyEntity enemy)
        {
            enemy.OnDeath -= HandleEnemyDeath;
            _enemiesAlive--;
            
            if (_enemiesAlive <= 0)
            {
                FinishWave();
            }
        }
        
        
        private void FinishWave()
        {
            _isWaveActive = false;
            UnityEngine.Debug.Log($"Wave {_currentWave} CLEARED!");
            OnWaveCompleted?.Invoke();
        }

        private Vector3 GetRandomPointAroundPlayer()
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized * 10f;
            Vector3 pos = _playerTransform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
            return pos;
        }
    }
}