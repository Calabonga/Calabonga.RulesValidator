namespace Calabonga.RulesValidator.Tests.TestSupport;

/// <summary>
/// Открывает protected internal AddResult для тестов из другой сборки.
/// </summary>
public sealed class TestAllTriggeredValidationResult<T> : AllTriggeredValidationResult<T>
{
    public TestAllTriggeredValidationResult(T entity) : base(entity)
    {
    }

    public void AddResultPublic(IValidatorResult<T> result) => AddResult(result);
}
