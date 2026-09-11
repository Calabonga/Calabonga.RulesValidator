using Calabonga.RulesValidator.Tests.TestSupport;

namespace Calabonga.RulesValidator.Tests;

public class NoErrorValidationResultTests
{
    [Fact]
    public void HasTriggered_Should_BeFalse_Always()
    {
        var result = new NoErrorValidationResult<Sample>(new Sample());

        Assert.False(result.HasTriggered);
    }

    [Fact]
    public void Errors_Should_BeEmpty_Always()
    {
        var result = new NoErrorValidationResult<Sample>(new Sample());

        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Entity_Should_ReturnProvidedEntity_When_Constructed()
    {
        var entity = new Sample { Name = "Bob" };

        var result = new NoErrorValidationResult<Sample>(entity);

        Assert.Same(entity, result.Entity);
    }
}
