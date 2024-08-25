namespace Casko.AspNetCore.XmlSiteMaps.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class PriorityValidationAttribute(float minValue = 0.1f, float maxValue = 1.0f, float step = 0.1f)
    : Attribute
{
    internal float MinValue { get; } = minValue;
    internal float MaxValue { get; } = maxValue;
    internal float Step { get; } = step;
}