using HealthChecks.API.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class ClientValidatorTests
{
    private readonly ClientValidator _clientValidator;

    public ClientValidatorTests()
    {
        _clientValidator = new ClientValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var clientToValidate = ClientBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _clientValidator.ValidateAsync(clientToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var clientWithInvalidName = ClientBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _clientValidator.ValidateAsync(clientWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            "aa",
            new string('a', 201)
        };

    [Theory]
    [MemberData(nameof(InvalidDescriptionParameters))]
    public async Task ValidateAsync_InvalidDescription_ReturnsFalse(string description)
    {
        // A
        var clientWithInvalidDescription = ClientBuilder.NewObject().WithDescription(description).DomainBuild();

        // A
        var validationResult = await _clientValidator.ValidateAsync(clientWithInvalidDescription);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidDescriptionParameters() =>
        new()
        {
            "",
            "aaaaa",
            new string('a', 2001)
        };


}
