using UnityEngine;

namespace Enemy.Infrastructure.Config
{
    [System.Serializable]
    public class EnemyScaling
    {
        public ScaleMode Mode = ScaleMode.Linear;
        public float Factor = 0.1f;

        [Tooltip("Logistic/Linear)")]
        public float MaxMultiplier = 10f;

        public float Evaluate(int wave)
        {
            switch (Mode)
            {
                case ScaleMode.Linear:
                    return Mathf.Min(1f + Factor * (wave - 1), MaxMultiplier);

                case ScaleMode.Exponential:
                    float exp = Mathf.Pow(1f + Factor, wave - 1);
                    return Mathf.Min(exp, MaxMultiplier);

                case ScaleMode.Logistic:
                    return MaxMultiplier / (1f + Mathf.Exp(-Factor * (wave - 10)));

                default:
                    return 1f;
            }
        }
    }
}