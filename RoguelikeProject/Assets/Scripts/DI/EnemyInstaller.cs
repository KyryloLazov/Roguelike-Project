using Enemy.Domain;
using Enemy.Infrastructure.Config;
using Enemy.Presentation;
using UnityEngine;
using UnityEngine.Serialization;
using WaveControl.Domain;
using WaveControl.Infrastructure.Config;

namespace DI
{
    public class EnemyInstaller : BaseBindings
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private WavesConfig wavesConfig;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private Transform _playerTransform;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_enemyConfig);
            Container.BindInstance(wavesConfig);
            
            Container.Bind<Transform>().WithId("Player").FromInstance(_playerTransform);
            
            Container.BindMemoryPool<EnemyEntity, EnemyPool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.Bind<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaveController>().AsSingle().NonLazy();
        }
    }
}