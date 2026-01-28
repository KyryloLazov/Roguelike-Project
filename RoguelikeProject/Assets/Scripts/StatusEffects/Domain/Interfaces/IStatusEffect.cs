using UnityEngine;

namespace StatusEffects.Domain.Interfaces
{
    public interface IStatusEffect
    {
        bool Tick(float deltaTime, GameObject target);
      
        string Id { get; }
        
        void OnApply(GameObject target);
        void OnRemove(GameObject target);
    }
}