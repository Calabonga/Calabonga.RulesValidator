using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples.PersonRules;

public class NameRequiredRule : ValidationRule<Person>
{
    public override string DisplayName => "Name is required";

    protected override Func<Person, bool> ThrowWhen()
    {
        return x => string.IsNullOrEmpty(x.Name);
    }
}