using System.Collections.Generic;
using UnityEngine;
using Weapons.Presentation;
using Zenject;

namespace Weapons.Domain.Pool
{
    public class WeaponProjectilePool
    {
        private readonly DiContainer _container;
        private readonly Transform _poolRoot;
        
        private readonly Dictionary<int, Queue<WeaponProjectile>> _pools = new();

        public WeaponProjectilePool(DiContainer container)
        {
            _container = container;
            _poolRoot = new GameObject("[Projectile Pool]").transform;
        }

        public WeaponProjectile Spawn(WeaponProjectile prefab, Vector3 position, Quaternion rotation)
        {
            int key = prefab.GetInstanceID();

            if (!_pools.ContainsKey(key))
            {
                _pools[key] = new Queue<WeaponProjectile>();
            }

            WeaponProjectile projectile;

            if (_pools[key].Count > 0)
            {
                projectile = _pools[key].Dequeue();
            }
            else
            {
                projectile = _container.InstantiatePrefabForComponent<WeaponProjectile>(prefab, _poolRoot);
            }
            
            projectile.transform.position = position;
            projectile.transform.rotation = rotation;
            projectile.gameObject.SetActive(true);
            
            projectile.SetPoolService(this, prefab);

            return projectile;
        }

        public void Despawn(WeaponProjectile projectile, WeaponProjectile originalPrefab)
        {
            projectile.gameObject.SetActive(false);
            projectile.transform.SetParent(_poolRoot);

            int key = originalPrefab.GetInstanceID();
            
            if (!_pools.ContainsKey(key))
            {
                _pools[key] = new Queue<WeaponProjectile>();
            }
            
            _pools[key].Enqueue(projectile);
        }
    }
}