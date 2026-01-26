using System.Collections.Generic;
using UnityEngine;
using Weapons.Domain.Pool;
using Weapons.Domain.Projectile.Interfaces;

namespace Weapons.Presentation
{
    public class WeaponProjectile : MonoBehaviour
    {
        private float _damage;
        private float _speed;
        private float _lifetime;
        
        private List<IProjectileModifier> _effects = new();
        private WeaponProjectilePool _pool;
        private WeaponProjectile _originalPrefab;
        
        public void SetPoolService(WeaponProjectilePool pool, WeaponProjectile prefab)
        {
            _pool = pool;
            _originalPrefab = prefab;
        }

        public void Initialize(float damage, float speed, float lifetime, List<IProjectileModifier> effects)
        {
            _damage = damage;
            _speed = speed;
            _lifetime = lifetime;
            _effects = effects ?? new List<IProjectileModifier>();
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
            
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0)
            {
                ReturnToPool();
                return;
            }
            
            for (int i = 0; i < _effects.Count; i++)
            {
                _effects[i].OnUpdate(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Damage();
            
            foreach (var effect in _effects)
            {
                effect.OnHit(this, other.gameObject, transform.position);
            }

            ReturnToPool();
        }
        
        private void ReturnToPool()
        {
            if (_pool != null)
            {
                _pool.Despawn(this, _originalPrefab);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}