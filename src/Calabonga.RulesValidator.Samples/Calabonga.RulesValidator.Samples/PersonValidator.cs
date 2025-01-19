using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples;

public class PersonValidator : RulesValidator<Person>
{
    /// <inheritdoc />
    public PersonValidator(IEnumerable<IValidationRule<Person>> rules, IValidatorConfiguration<Person> configuration)
        : base(rules, configuration)
    {
    }
}