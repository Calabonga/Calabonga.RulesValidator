namespace Calabonga.RulesValidator.Tests.TestSupport;

/// <summary>
/// Тестовая реализация <see cref="ValidationRule{T}"/> с настраиваемым условием срабатывания.
/// </summary>
public sealed class PredicateRule : ValidationRule<Sample>
{
    private readonly Func<Sample, bool> _predicate;

    public PredicateRule(Func<Sample, bool> predicate, string displayName = "Predicate rule", int orderIndex = 0)
    {
        _predicate = predicate;
        DisplayName = displayName;
        OrderIndex = orderIndex;
    }

    public override string DisplayName { get; }

    public override int OrderIndex { get; }

    protected override Func<Sample, bool> ThrowWhen() => _predicate;
}
