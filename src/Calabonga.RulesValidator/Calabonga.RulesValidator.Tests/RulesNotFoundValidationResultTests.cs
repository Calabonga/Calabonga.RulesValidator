using Calabonga.RulesValidator.Tests.TestSupport;

namespace Calabonga.RulesValidator.Tests;

public class RulesNotFoundValidationResultTests
{
    [Fact]
    public void HasTriggered_Should_BeFalse_Always()
    {
        var result = new RulesNotFoundValidationResult<Sample>(new Sample());

        Assert.False(result.HasTriggered);
    }

    [Fact]
    public void Errors_Should_ContainSingleMessage_Always()
    {
        var result = new RulesNotFoundValidationResult<Sample>(new Sample());

        Assert.Single(result.Errors);
    }
}
