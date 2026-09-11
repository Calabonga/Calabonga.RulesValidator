using Calabonga.RulesValidator.Tests.TestSupport;

namespace Calabonga.RulesValidator.Tests;

public class ValidatorConfigurationTests
{
    [Fact]
    public void ValidatorMode_Should_DefaultToFirst_When_NotSet()
    {
        var configuration = new ValidatorConfiguration<Sample>();

        Assert.Equal(ValidatorMode.First, configuration.ValidatorMode);
    }

    [Fact]
    public void ValidatorMode_Should_ReturnAssignedValue_When_Set()
    {
        var configuration = new ValidatorConfiguration<Sample> { ValidatorMode = ValidatorMode.All };

        Assert.Equal(ValidatorMode.All, configuration.ValidatorMode);
    }
}
