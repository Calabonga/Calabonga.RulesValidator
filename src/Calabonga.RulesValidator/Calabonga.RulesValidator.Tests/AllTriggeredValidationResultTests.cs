using Calabonga.RulesValidator.Tests.TestSupport;
using Moq;

namespace Calabonga.RulesValidator.Tests;

public class AllTriggeredValidationResultTests
{
    [Fact]
    public void HasTriggered_Should_BeFalse_When_NoResultsAdded()
    {
        var result = new TestAllTriggeredValidationResult<Sample>(new Sample());

        Assert.False(result.HasTriggered);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void HasTriggered_Should_BeTrue_When_ResultAdded()
    {
        var result = new TestAllTriggeredValidationResult<Sample>(new Sample());
        var added = Mock.Of<IValidatorResult<Sample>>(x => x.Errors == new[] { "error-1" });

        result.AddResultPublic(added);

        Assert.True(result.HasTriggered);
    }

    [Fact]
    public void Errors_Should_AggregateErrorsFromAllAddedResults_When_MultipleResultsAdded()
    {
        var result = new TestAllTriggeredValidationResult<Sample>(new Sample());
        var first = Mock.Of<IValidatorResult<Sample>>(x => x.Errors == new[] { "error-1" });
        var second = Mock.Of<IValidatorResult<Sample>>(x => x.Errors == new[] { "error-2", "error-3" });

        result.AddResultPublic(first);
        result.AddResultPublic(second);

        Assert.Equal(["error-1", "error-2", "error-3"], result.Errors);
    }

    [Fact]
    public void AddResult_Should_Ignore_When_ResultIsNull()
    {
        var result = new TestAllTriggeredValidationResult<Sample>(new Sample());

        result.AddResultPublic(null!);

        Assert.False(result.HasTriggered);
    }
}
