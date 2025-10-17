namespace Analytics.Contracts.Options;

public sealed class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";
    public string Default { get; init; } = default!;
}
