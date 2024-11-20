using Bogus;
using FluentAssertions;
using FudamentosTestes.Db;
using FudamentosTestes.Handlers;
using FudamentosTestes.Handlers.Exceptions;
using FudamentosTestes.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace FudamentosTestes.Tests.Handlers.Handlers;

[Trait("Category", "AddCarCommandHandlerTest")]
public class AddCarCommandHandlerTest
{
    private readonly Faker _faker = new("pt_BR");
    private readonly AddCarCommandHandler _handler;
    private readonly ICarChassiValidatorService _mockCarChassiValidatorService;
    private readonly AppDbContext _appDbContext;
    
    public AddCarCommandHandlerTest()
    {
        // App Db Context
        var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
        dbContextOptions.UseInMemoryDatabase("FundamentosTest");
        _appDbContext = new AppDbContext(dbContextOptions.Options);
        
        _mockCarChassiValidatorService = Substitute.For<ICarChassiValidatorService>();
        
        _handler = new AddCarCommandHandler(_mockCarChassiValidatorService, _appDbContext);
    }

    [Fact]
    public async Task Handle_GivenChassiInvalid_ThenShouldThrowException()
    {
        // Given invalid chassi command
        var carName = _faker.Vehicle.Model();
        var invalidCommand = new AddCarCommand(carName);

        _mockCarChassiValidatorService
            .CheckIfValidAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
        
        //When handle
        var action = () => _handler.Handle(invalidCommand, CancellationToken.None);

        var expectedMessage = $"[{carName}] chassi invalido!";
        await action.Should().ThrowAsync<InvalidChassiException>(expectedMessage);
    }

    [Fact]
    public async Task Handle_Given_ChassiValid_ThenShouldInsertAndReturnNewCar()
    {
        var carName = _faker.Vehicle.Model();
        var validCommand = new AddCarCommand(carName);

        _mockCarChassiValidatorService
            .CheckIfValidAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        var result = await _handler.Handle(validCommand, CancellationToken.None);

        result.Name.Should().Be(carName);
        result.Id.Should().NotBeEmpty();

        var carId = result.Id;
        var insertedCar = await _appDbContext.Cars.SingleOrDefaultAsync(x => x.Id == carId, CancellationToken.None);

        insertedCar.Should().NotBeNull();
        insertedCar!.Name.Should().Be(carName);
    }
}