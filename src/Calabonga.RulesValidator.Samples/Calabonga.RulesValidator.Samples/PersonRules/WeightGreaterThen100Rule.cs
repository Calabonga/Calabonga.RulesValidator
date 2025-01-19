using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples.PersonRules;

public class WeightGreaterThen100Rule : ValidationRule<Person>
{
    public override string DisplayName => "Weight is greater then 100";

    protected override Func<Person, bool> ThrowWhen()
    {
        return x => x.Weight > 100;
    }
}