using Calabonga.DemoClasses;

namespace Calabonga.RulesValidator.Samples.PersonRules;

public class WeightLessThen50Rule : ValidationRule<Person>
{
    public override string DisplayName => "Weight is less then 50";

    protected override Func<Person, bool> ThrowWhen()
    {
        return x => x.Weight < 50;
    }
}