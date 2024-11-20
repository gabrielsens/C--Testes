using Bogus;
using FluentAssertions;
using FudamentosTestes.Handlers.Exceptions;
using Xunit.Abstractions;

namespace FudamentosTestes.Tests.Handlers.Exceptions;

[Trait("Category", "InvalidChassiExceptionTests")]
public class InvalidChassiExceptionTests(ITestOutputHelper testOutputHelper)
{
    private readonly Faker _faker = new("pt_BR");
    
    [Fact]
    public void Constructor_GivenMessage_ThenShouldSetMessageToException()
    {
        // Arrange
        var message = _faker.Lorem.Text();
        testOutputHelper.WriteLine(message);
        
        // Act
        var exception = new InvalidChassiException(message);

        // Assert
        exception.Message.Should().Be(message);
    }
}