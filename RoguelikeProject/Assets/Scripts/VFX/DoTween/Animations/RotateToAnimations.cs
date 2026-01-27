using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "RotateToAnimation", menuName = "Configs/VFX/Animations/Rotate To")]
public class RotateToAnimation : VFXAnimation
{
    [SerializeField] private Vector3 _endEulerAngles;
    [SerializeField] private float _duration = 1f;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        sequence
            .Append(target.DORotate(_endEulerAngles, _duration).SetEase(Ease.InOutQuad));
    }
}