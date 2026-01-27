public struct FadeParameters : IVFXParameters, IWithDelay
{
    public float? EndAlpha;
    public float? Duration;
    public float? Delay { get; set; }
}