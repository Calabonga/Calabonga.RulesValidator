using Calabonga.RulesValidator.Tests.TestSupport;
using Moq;

namespace Calabonga.RulesValidator.Tests;

public class RulesValidatorExtensionsTests
{
    [Fact]
    public void GetTriggered_Should_ReturnEmptyList_When_SourceIsEmpty()
    {
        var source = new List<IValidationRule<Sample>>();

        var triggered = source.GetTriggered();

        Assert.Empty(triggered);
    }

    [Fact]
    public void GetTriggered_Should_ReturnOnlyTriggeredRules_When_SourceHasMixedRules()
    {
        var triggeredRule = Mock.Of<IValidationRule<Sample>>(x => x.IsTriggered == true);
        var notTriggeredRule = Mock.Of<IValidationRule<Sample>>(x => x.IsTriggered == false);
        var source = new List<IValidationRule<Sample>> { triggeredRule, notTriggeredRule };

        var triggered = source.GetTriggered();

        Assert.Equal([triggeredRule], triggered);
    }
}
