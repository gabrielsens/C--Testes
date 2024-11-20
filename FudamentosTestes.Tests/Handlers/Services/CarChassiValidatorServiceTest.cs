using System.Diagnostics;
using FluentAssertions;
using FudamentosTestes.Services;
using Xunit.Abstractions;

namespace FudamentosTestes.Tests.Handlers.Services;

[Trait("Category", "CarChassiValidatorServiceTest")]
public class CarChassiValidatorServiceTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldReturnTrueAsync()
    {
        // arrange
        var anyId = Guid.NewGuid();
        var validator = new CarChassiValidatorService();
        
        // act
        var result = await validator.CheckIfValidAsync(anyId, CancellationToken.None);
        
        // Then should return true
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldTakeMoreThanTwoSecondsAsync()
    {
        // arrange
        var anyId = Guid.NewGuid();
        var validator = new CarChassiValidatorService();
        const int minExpectedTimeElapsed = 2000;
        var action = () => validator.CheckIfValidAsync(anyId, CancellationToken.None);
        
        // act
        var stopWatch = Stopwatch.StartNew();
        var result = await action();
        stopWatch.Stop();

        testOutputHelper.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds.ToString()}");
        
        // Then should return more than two seconds
        result.Should().BeTrue();
        stopWatch.ElapsedMilliseconds.Should().BeGreaterThan(minExpectedTimeElapsed);
    }
    
    [Fact]
    public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldTakeLessThanFourSecondsAsync()
    {
        // arrange
        var anyId = Guid.NewGuid();
        var validator = new CarChassiValidatorService();
        const int maxExpectedTimeElapsed = 4000;
        var action = () => validator.CheckIfValidAsync(anyId, CancellationToken.None);
        
        // act
        var stopWatch = Stopwatch.StartNew();
        var result = await action();
        stopWatch.Stop();
        
        testOutputHelper.WriteLine($"Elapsed Time {stopWatch.ElapsedMilliseconds.ToString()}");

        // Then should return more than two seconds
        result.Should().BeTrue();
        stopWatch.ElapsedMilliseconds.Should().BeLessThan(maxExpectedTimeElapsed);
    }
}