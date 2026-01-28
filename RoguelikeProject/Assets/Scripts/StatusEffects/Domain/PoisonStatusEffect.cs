using StatusEffects.Domain.Interfaces;
using UnityEngine;

namespace StatusEffects.Domain
{
    public class PoisonStatusEffect : IStatusEffect
    {
        public string Id => "Poison";

        private readonly float _damagePerTick;
        private readonly float _duration;
        private readonly float _tickInterval = 1f;

        private float _timeElapsed;
        private float _tickTimer;

        public PoisonStatusEffect(float damagePerTick, float duration)
        {
            _damagePerTick = damagePerTick;
            _duration = duration;
        }

        public void OnApply(GameObject target)
        {
            UnityEngine.Debug.Log($"Poison Applied to {target.name}");
        }

        public bool Tick(float deltaTime, GameObject target)
        {
            _timeElapsed += deltaTime;
            _tickTimer += deltaTime;

            if (_tickTimer >= _tickInterval)
            {
                if (target.TryGetComponent(out IDamageable health))
                {
                    health.TakeDamage(_damagePerTick);
                }
                _tickTimer = 0;
            }

            return _timeElapsed >= _duration;
        }

        public void OnRemove(GameObject target)
        {
            UnityEngine.Debug.Log($"Poison Removed from {target.name}");
        }
    }
}