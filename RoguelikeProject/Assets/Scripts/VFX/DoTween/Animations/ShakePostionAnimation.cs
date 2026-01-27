using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ShakePositionAnimation", menuName = "Configs/VFX/Animations/Shake Position")]
public class ShakePositionAnimation : VFXAnimation
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _strength = 1f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private float _randomness = 90f;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        sequence
            .Append(target.DOShakePosition(
                _duration,
                _strength,
                _vibrato,
                _randomness
            ));
    }
}