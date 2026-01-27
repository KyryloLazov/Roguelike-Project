using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToAnimation", menuName = "Configs/VFX/Animations/Move To")]
public class MoveToAnimation : VFXAnimation
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private bool _isRelative;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        Vector3 dest = _isRelative
            ? target.position + _endPosition
            : _endPosition;
        sequence
            .Append(target.DOMove(dest, _duration).SetEase(Ease.InOutSine));
    }
}