using System;

namespace Player.Domain.PlayerStats
{
    public class RuntimeStat
    {
        public event Action<float> OnValueChanged;

        public float Value { get; private set; }
        public float BaseValue { get; private set; }

        public RuntimeStat(float baseValue)
        {
            BaseValue = baseValue;
            Value = baseValue;
        }

        public void SetValue(float newValue)
        {
            if (Value == newValue) return;
            
            Value = newValue;
            OnValueChanged?.Invoke(Value);
        }

        public void Modify(float amount)
        {
            SetValue(Value + amount);
        }
    }
}