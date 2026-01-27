using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpAnimation", menuName = "Configs/VFX/Animations/Jump")]
public class JumpAnimation : VFXAnimation
{
    [SerializeField] private float _jumpPower = 2f;
    [SerializeField] private int _numJumps = 1;
    [SerializeField] private float _duration = 1f;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        Vector3 end = target.position + Vector3.up * 0.1f;
        sequence.Append(target.DOJump(
                end,
                _jumpPower,
                _numJumps,
                _duration
            ));
    }
}