using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "UIButtonClickAnimation", menuName = "Configs/VFX/Animations/UI Button Click")]
public class UIButtonClickAnimation : VFXAnimation
{
    [Header("Default Values")]
    [SerializeField] private float _pressScale = 0.9f;
    [SerializeField] private float _pressDuration = 0.08f;
    [SerializeField] private Ease _pressEase = Ease.OutQuad;
    [SerializeField] private float _releaseDuration = 0.2f;
    [SerializeField] private Ease _releaseEase = Ease.OutElastic;
    [SerializeField] private bool _resetStartScale  = true;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        if (_resetStartScale)
            sequence.AppendCallback(() =>
                target.localScale = Vector3.one
            );

        sequence.Append(
            target
               .DOScale(_pressScale, _pressDuration)
               .SetEase(_pressEase)
        );
        
        sequence.Append(
            target
               .DOScale(1f, _releaseDuration)
               .SetEase(_releaseEase)
        );
    }
}