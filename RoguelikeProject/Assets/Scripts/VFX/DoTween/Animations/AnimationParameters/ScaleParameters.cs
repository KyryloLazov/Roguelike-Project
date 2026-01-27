using DG.Tweening;

public struct ScaleParams : IVFXParameters, IWithDelay
{
    public float? StartScale;
    public float? EndScale;
    public float? Duration;
    public Ease? Ease;
    public bool? ResetStartScale;
    public float? Delay { get; set; }
}