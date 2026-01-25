using Enemy.Presentation;
using UnityEngine;
using Zenject;

namespace Enemy.Domain
{
    public class EnemyPool : MonoMemoryPool<EnemyStats, EnemyEntity>
    {
    }

    public class EnemySpawner
    {
        private readonly EnemyPool _pool;

        public EnemySpawner(EnemyPool pool)
        {
            _pool = pool;
        }

        public EnemyEntity Spawn(EnemyStats stats, Vector3 position)
        {
            EnemyEntity enemy = _pool.Spawn(stats);
            enemy.transform.position = position;
            return enemy;
        }
    }
}