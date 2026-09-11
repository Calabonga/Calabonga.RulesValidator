using Calabonga.RulesValidator.Tests.TestSupport;

namespace Calabonga.RulesValidator.Tests;

public class ValidationRuleTests
{
    [Fact]
    public async Task ValidateAsync_Should_ThrowArgumentNullException_When_EntityIsNull()
    {
        var rule = new PredicateRule(_ => true);

        await Assert.ThrowsAsync<ArgumentNullException>(() => rule.ValidateAsync(null!));
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnErrorResult_When_PredicateIsTrue()
    {
        var entity = new Sample { Name = "Bob", Age = 5 };
        var rule = new PredicateRule(x => x.Age < 18, "Too young");

        var result = await rule.ValidateAsync(entity);

        Assert.True(result.HasTriggered);
        Assert.Same(entity, result.Entity);
        Assert.Contains(result.Errors, e => e.Contains("Too young"));
    }

    [Fact]
    public async Task ValidateAsync_ShouldNot_ReturnErrorResult_When_PredicateIsFalse()
    {
        var entity = new Sample { Name = "Alice", Age = 30 };
        var rule = new PredicateRule(x => x.Age < 18);

        var result = await rule.ValidateAsync(entity);

        Assert.False(result.HasTriggered);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Name_Should_ReturnTypeName_When_NotOverridden()
    {
        var rule = new PredicateRule(_ => false);

        Assert.Equal(nameof(PredicateRule), rule.Name);
    }

    [Fact]
    public void IsTriggered_Should_BeFalse_When_RuleJustCreated()
    {
        var rule = new PredicateRule(_ => true);

        Assert.False(rule.IsTriggered);
    }

    [Fact]
    public void OrderIndex_Should_ReturnZero_When_NotOverridden()
    {
        var rule = new PredicateRule(_ => true);

        Assert.Equal(0, rule.OrderIndex);
    }
}
