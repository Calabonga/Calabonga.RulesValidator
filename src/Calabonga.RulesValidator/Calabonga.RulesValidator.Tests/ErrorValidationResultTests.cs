using Calabonga.RulesValidator.Tests.TestSupport;
using Moq;

namespace Calabonga.RulesValidator.Tests;

public class ErrorValidationResultTests
{
    [Fact]
    public void HasTriggered_Should_BeTrue_When_RuleIsProvided()
    {
        var rule = Mock.Of<IRule>();
        var entity = new Sample();

        var result = new ErrorValidationResult<Sample>(rule, entity);

        Assert.True(result.HasTriggered);
        Assert.Same(entity, result.Entity);
    }

    [Fact]
    public void HasTriggered_ShouldNot_BeTrue_When_RuleIsNull()
    {
        var result = new ErrorValidationResult<Sample>(null!, new Sample());

        Assert.False(result.HasTriggered);
    }

    [Fact]
    public void Errors_Should_ContainDisplayNameAndName_When_RuleIsProvided()
    {
        var ruleMock = new Mock<IRule>();
        ruleMock.SetupGet(x => x.DisplayName).Returns("Display Name");
        ruleMock.SetupGet(x => x.Name).Returns("RuleName");

        var result = new ErrorValidationResult<Sample>(ruleMock.Object, new Sample());

        Assert.Equal(["Display Name (RuleName)"], result.Errors);
    }

    [Fact]
    public void Errors_Should_BeEmpty_When_RuleIsNull()
    {
        var result = new ErrorValidationResult<Sample>(null!, new Sample());

        Assert.Empty(result.Errors);
    }
}
