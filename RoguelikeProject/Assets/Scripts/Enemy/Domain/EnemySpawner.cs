using Enemy.Presentation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Domain
{
    public class EnemyPool : MonoMemoryPool<EnemyEntity> { }

    public class EnemySpawner
    {
        private readonly EnemyPool _pool;

        public EnemySpawner(EnemyPool pool)
        {
            _pool = pool;
        }

        public EnemyEntity Spawn(EnemyStats stats, Vector3 position)
        {
            EnemyEntity enemy = _pool.Spawn();
            var agent = enemy.GetComponent<NavMeshAgent>();
            
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Could not find NavMesh near {position}. Spawning at original position (might fail).");
                agent.Warp(position);
            }

            enemy.Initialize(stats);
            return enemy;
        }
    }
}