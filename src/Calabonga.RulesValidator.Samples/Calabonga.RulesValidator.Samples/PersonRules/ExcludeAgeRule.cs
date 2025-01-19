using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples.PersonRules;

public class ExcludeAgeRule : DynamicValidationRule<Person, int>
{
    public ExcludeAgeRule(int parameter) : base(parameter)
    {
    }

    public override string DisplayName => $"Should not equals {Parameter}";

    protected override Func<Person, bool> ThrowWhen()
    {
        return x => x.Age == Parameter;
    }
}