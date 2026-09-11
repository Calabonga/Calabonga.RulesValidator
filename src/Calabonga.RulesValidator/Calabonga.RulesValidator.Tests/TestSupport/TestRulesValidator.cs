namespace Calabonga.RulesValidator.Tests.TestSupport;

/// <summary>
/// Конкретный валидатор для тестирования <see cref="RulesValidator{T}"/> (класс абстрактный).
/// </summary>
public sealed class TestRulesValidator : RulesValidator<Sample>
{
    public TestRulesValidator(IEnumerable<IValidationRule<Sample>> rules, IValidatorConfiguration<Sample> configuration)
        : base(rules, configuration)
    {
    }
}
