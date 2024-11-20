using Bogus;
using FluentAssertions;
using FudamentosTestes.Entities;
using Xunit.Abstractions;

namespace FudamentosTestes.Tests.Entities;

[Trait("Category", "CarTests")]
public sealed class CarTests(ITestOutputHelper testOutputHelper)
{
    private readonly Faker _faker = new("pt_BR");

    [Theory]
    [InlineData("Carro 1")]
    [InlineData("Carro 2")]
    public void TheoryConstructor_GivenAllParameters_ThenShouldSetThePropertiesCorrectly(string expectedCarName)
    {
        // Given All Parameters
        // Arrange
        var expectedId = Guid.NewGuid();
        
        // Act
        var carro = new Car(expectedId, expectedCarName);

        // Then should set the properties correctly
        // Assert
        Assert.Equal(expectedId, carro.Id);
        Assert.Equal(expectedCarName, carro.Name);
    }
    
    [Fact]
    public void NoFakerConstructor_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
    {
        // Given All Parameters
        // Arrange
        var expectedCarName = "Carro";
        var expectedId = Guid.NewGuid();
        
        // Act
        var carro = new Car(expectedId, expectedCarName);

        // Then should set the properties correctly
        // Assert
        Assert.Equal(expectedId, carro.Id);
        Assert.Equal(expectedCarName, carro.Name);
    }
    
    [Fact]
    public void FakerConstructor_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
    {
        // Given All Parameters
        // Arrange
        var expectedCarName = _faker.Person.FirstName;
        var expectedId = Guid.NewGuid();
        testOutputHelper.WriteLine(expectedCarName);
        
        // Act
        var carro = new Car(expectedId, expectedCarName);

        // Then should set the properties correctly
        // Assert
        Assert.Equal(expectedId, carro.Id);
        Assert.Equal(expectedCarName, carro.Name);
    }
    
    [Fact]
    public void FluentAssertionsConstructor_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
    {
        // Given All Parameters
        // Arrange
        var expectedCarName = _faker.Person.FirstName;
        var expectedId = Guid.NewGuid();
        testOutputHelper.WriteLine(expectedCarName);
        
        // Act
        var carro = new Car(expectedId, expectedCarName);

        // Then should set the properties correctly
        // Assert
        // Assert.Equal(expectedId, carro.Id);
        // Assert.Equal(expectedCarName, carro.Name);
        carro.Id.Should().Be(expectedId, "Should be equal");
        carro.Name.Should().Be(expectedCarName);
    }
}