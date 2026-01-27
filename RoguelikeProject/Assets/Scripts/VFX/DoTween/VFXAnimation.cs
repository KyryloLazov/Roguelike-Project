using DG.Tweening;
using UnityEngine;

public abstract class VFXAnimation : ScriptableObject
{
    [field: SerializeField] public float Delay { get; private set; }
    public Sequence CreateSequence(Transform target, IVFXParameters parameters = null)
    {
        Sequence sequence = DOTween.Sequence();

        float finalDelay = Delay;
        
        if (parameters is IWithDelay delayedParams && delayedParams.Delay.HasValue)
        {
            finalDelay = delayedParams.Delay.Value;
        }

        if (finalDelay > 0)
        {
            sequence.AppendInterval(finalDelay);
        }

        BuildAnimation(sequence, target, parameters);
        return sequence;
    }

    protected abstract void BuildAnimation(
        Sequence sequence,
        Transform target,
        IVFXParameters parameters
    );
}

public interface IWithDelay
{
    float? Delay { get; }
}