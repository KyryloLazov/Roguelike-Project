using System;
using Enemy.Domain;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Presentation
{
    public class EnemyEntity : MonoBehaviour, IDamageable
    {
        private NavMeshAgent _agent;
        private Transform _target; 
        private EnemyStats _stats;
        private float _currentHealth;
        [Inject] private EnemyPool _pool;
        
        public event Action<EnemyEntity> OnDeath;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false; 
            _agent.updateUpAxis = false;
        }
        
        public void Initialize(EnemyStats stats)
        {
            _stats = stats;
            _currentHealth = _stats.Health;
            _agent.speed = _stats.Speed;
        
            UnityEngine.Debug.Log($"Initialized enemy. HP: {_currentHealth}");
            gameObject.SetActive(true);
        }
        
        public void OnDespawned()
        {
            OnDeath = null;
            gameObject.SetActive(false);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null || !_agent.isOnNavMesh) return;
            
            _agent.SetDestination(_target.position);
            
            if (_agent.velocity.sqrMagnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
            }

        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.GetComponent<IDamageable>().TakeDamage(_stats.Damage);
            }
        }

        private void Die()
        {
            OnDeath?.Invoke(this);
            _pool.Despawn(this);
        }
    }
}