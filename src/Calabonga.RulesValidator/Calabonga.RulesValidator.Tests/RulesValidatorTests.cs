using Calabonga.RulesValidator.Tests.TestSupport;

namespace Calabonga.RulesValidator.Tests;

public class RulesValidatorTests
{
    [Fact]
    public async Task ValidateAsync_Should_ReturnRulesNotFoundResult_When_NoRulesAndNoDynamicRules()
    {
        var validator = CreateValidator([], ValidatorMode.First);

        var result = await validator.ValidateAsync(new Sample());

        Assert.IsType<RulesNotFoundValidationResult<Sample>>(result);
        Assert.False(result.HasTriggered);
    }

    [Fact]
    public async Task ValidateAsync_ShouldNot_ReturnRulesNotFoundResult_When_OnlyDynamicRulesProvided()
    {
        var validator = CreateValidator([], ValidatorMode.First);
        var dynamicRule = new PredicateRule(_ => true);

        var result = await validator.ValidateAsync(new Sample(), [dynamicRule]);

        Assert.IsNotType<RulesNotFoundValidationResult<Sample>>(result);
        Assert.True(result.HasTriggered);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnNoErrorResult_When_FirstModeAndNoRuleTriggered()
    {
        var rule = new PredicateRule(_ => false);
        var validator = CreateValidator([rule], ValidatorMode.First);

        var result = await validator.ValidateAsync(new Sample());

        Assert.IsType<NoErrorValidationResult<Sample>>(result);
        Assert.False(rule.IsTriggered);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnOnlyFirstTriggeredRuleResult_When_FirstModeAndMultipleRulesTrigger()
    {
        var firstRule = new PredicateRule(_ => true, "First", orderIndex: 0);
        var secondRule = new PredicateRule(_ => true, "Second", orderIndex: 1);
        var validator = CreateValidator([secondRule, firstRule], ValidatorMode.First);

        var result = await validator.ValidateAsync(new Sample());

        Assert.Contains(result.Errors, e => e.Contains("First"));
        Assert.DoesNotContain(result.Errors, e => e.Contains("Second"));
        Assert.True(firstRule.IsTriggered);
        Assert.False(secondRule.IsTriggered);
    }

    [Fact]
    public async Task ValidateAsync_Should_ReturnAllTriggeredRulesResult_When_AllModeAndMultipleRulesTrigger()
    {
        var firstRule = new PredicateRule(_ => true, "First", orderIndex: 0);
        var secondRule = new PredicateRule(_ => true, "Second", orderIndex: 1);
        var notTriggeredRule = new PredicateRule(_ => false, "Third", orderIndex: 2);
        var validator = CreateValidator([firstRule, secondRule, notTriggeredRule], ValidatorMode.All);

        var result = await validator.ValidateAsync(new Sample());

        Assert.IsType<AllTriggeredValidationResult<Sample>>(result);
        Assert.Contains(result.Errors, e => e.Contains("First"));
        Assert.Contains(result.Errors, e => e.Contains("Second"));
        Assert.DoesNotContain(result.Errors, e => e.Contains("Third"));
        Assert.True(firstRule.IsTriggered);
        Assert.True(secondRule.IsTriggered);
        Assert.False(notTriggeredRule.IsTriggered);
    }

    [Fact]
    public void Mode_Should_ReturnModeFromConfiguration_When_Requested()
    {
        var validator = CreateValidator([], ValidatorMode.All);

        Assert.Equal(ValidatorMode.All, validator.Mode);
    }

    [Fact]
    public void HasRules_Should_BeFalse_When_NoRulesProvided()
    {
        var validator = CreateValidator([], ValidatorMode.First);

        Assert.False(validator.HasRules);
    }

    [Fact]
    public void HasRules_Should_BeTrue_When_RulesProvided()
    {
        var validator = CreateValidator([new PredicateRule(_ => true)], ValidatorMode.First);

        Assert.True(validator.HasRules);
    }

    [Fact]
    public void AddRules_Should_SetRules_When_ValidatorHasNoRulesYet()
    {
        var validator = CreateValidator([], ValidatorMode.First);
        var rule = new PredicateRule(_ => true);

        validator.AddRules([rule]);

        Assert.True(validator.HasRules);
        Assert.Same(rule, Assert.Single(validator.Rules));
    }

    [Fact]
    public void AddRules_ShouldNot_ReplaceExistingRules_When_ValidatorAlreadyHasRules()
    {
        var existingRule = new PredicateRule(_ => true);
        var validator = CreateValidator([existingRule], ValidatorMode.First);

        validator.AddRules([new PredicateRule(_ => false)]);

        Assert.Same(existingRule, Assert.Single(validator.Rules));
    }

    [Fact]
    public void AddRules_Should_ThrowArgumentNullException_When_RulesIsNull()
    {
        var validator = CreateValidator([], ValidatorMode.First);

        Assert.Throws<ArgumentNullException>(() => validator.AddRules(null!));
    }

    private static TestRulesValidator CreateValidator(IEnumerable<IValidationRule<Sample>> rules, ValidatorMode mode) =>
        new(rules, new ValidatorConfiguration<Sample> { ValidatorMode = mode });
}
