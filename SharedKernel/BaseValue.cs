namespace SharedKernel;

public abstract record BaseValue<T>(T Value) : IValue
{
    public override string ToString() => Value?.ToString() ?? string.Empty;
}
