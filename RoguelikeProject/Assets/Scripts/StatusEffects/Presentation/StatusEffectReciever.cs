using System.Collections.Generic;
using StatusEffects.Domain.Interfaces;
using UnityEngine;

namespace StatusEffects.Presentation
{
    public class StatusEffectReceiver : MonoBehaviour
    {
        private readonly List<IStatusEffect> _activeEffects = new();
        private readonly List<IStatusEffect> _effectsToRemove = new(); 

        public void ApplyEffect(IStatusEffect effect)
        {
            var existing = _activeEffects.Find(e => e.Id == effect.Id);
            if (existing != null)
            {
                existing.OnRemove(gameObject);
                _activeEffects.Remove(existing);
            }

            effect.OnApply(gameObject);
            _activeEffects.Add(effect);
        }

        private void Update()
        {
            if (_activeEffects.Count == 0) return;

            foreach (var effect in _activeEffects)
            {
                bool isFinished = effect.Tick(Time.deltaTime, gameObject);
                if (isFinished)
                {
                    _effectsToRemove.Add(effect);
                }
            }

            if (_effectsToRemove.Count > 0)
            {
                foreach (var effect in _effectsToRemove)
                {
                    effect.OnRemove(gameObject);
                    _activeEffects.Remove(effect);
                }
                _effectsToRemove.Clear();
            }
        }
    }
}