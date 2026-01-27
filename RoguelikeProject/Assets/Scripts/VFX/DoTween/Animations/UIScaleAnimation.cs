using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "UIScaleAnimation", menuName = "Configs/VFX/Animations/UI Scale")]
public class UIScaleAnimation : VFXAnimation
{
    [Header("Default Values")]
    [SerializeField] private float _startScale = 1f;
    [SerializeField] private float _endScale = 0f;
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private Ease _ease = Ease.InBack;
    [SerializeField] private bool _resetStartScale  = true;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        float start = _startScale;
        float end = _endScale;
        float dur = _duration;
        Ease ease = _ease;
        bool reset = _resetStartScale;
        
        if (parameters is ScaleParams p)
        {
            start = p.StartScale ?? start;
            end = p.EndScale ?? end;
            dur = p.Duration ?? dur;
            ease = p.Ease ?? ease;
            reset = p.ResetStartScale ?? reset;
        }

        target.localScale = Vector3.one * start;
        
        if (reset)
            sequence.AppendCallback(() =>
                target.localScale = Vector3.one * start
            );
        
        sequence.Append(
            target.DOScale(end, dur)
                .SetEase(ease)
        );
    }
}