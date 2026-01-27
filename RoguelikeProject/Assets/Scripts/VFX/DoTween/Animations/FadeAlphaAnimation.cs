using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "FadeAlphaAnimation", menuName = "Configs/VFX/Animations/Fade Alpha")]
public class FadeAlphaAnimation : VFXAnimation
{
    [SerializeField] private float _endAlpha = 0f;
    [SerializeField] private float _duration = 0.5f;

    protected override void BuildAnimation(Sequence sequence, Transform target, IVFXParameters parameters)
    {
        if (parameters is FadeParameters fade)
        {
            _endAlpha = fade.EndAlpha ?? _endAlpha;
            _duration = fade.Duration ?? _duration;
        }
        
        var cg = target.GetComponent<CanvasGroup>();
        if (cg != null)
            sequence.Append(cg.DOFade(_endAlpha, _duration));
    }
}