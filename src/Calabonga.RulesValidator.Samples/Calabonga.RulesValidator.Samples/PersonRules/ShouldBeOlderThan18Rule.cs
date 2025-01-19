using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples.PersonRules;

public class ShouldBeOlderThan18Rule : ValidationRule<Person>
{
    public override string DisplayName => "Person is very young 18";

    protected override Func<Person, bool> ThrowWhen()
    {
        return x => x.Age < 18;
    }
}