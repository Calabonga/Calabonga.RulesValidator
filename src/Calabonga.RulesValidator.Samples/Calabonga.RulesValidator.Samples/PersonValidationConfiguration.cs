using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples;

public class PersonValidationConfiguration : IValidatorConfiguration<Person>
{
    /// <inheritdoc />
    public ValidatorMode ValidatorMode { get; set; } = ValidatorMode.All;
}