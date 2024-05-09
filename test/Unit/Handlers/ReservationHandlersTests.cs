using CloudHotel.Application.Abstractions.Persistence;
using CloudHotel.Application.Reservations.CreateReservation;
using CloudHotel.Application.Reservations.UpdateReservation;
using CloudHotel.Domain.GuestAggregate;
using CloudHotel.Domain.ReservationAggregate;
using FluentAssertions;
using Nett.Core;
using NSubstitute;

namespace CloudHotel.Unit.Tests.Reservations;

public sealed class ReservationHandlersTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Fixture _fixture;

    public ReservationHandlersTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _guestRepository = Substitute.For<IGuestRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _fixture = new Fixture();
        _fixture.Register(() => DateOnly.Parse("2025-01-01"));
        _fixture.Register(() => Guid.NewGuid().ToString());
        _fixture.Register(() => new Random().Next(0, 4));
    }
    
    [Fact]
    public async Task CreateReservationHandler_IndisponibleRoom_ShouldReturnError()
    {
        //Arrange
        var handler = new CreateReservationHandler(_reservationRepository, _unitOfWork, _guestRepository);
        var command = _fixture.Create<CreateReservationCommand>();

        _reservationRepository.Count(default, default, command.Arrival, command.Departure).ReturnsForAnyArgs(1);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public async Task CreateReservationHandler_DisponibleRoom_ShouldReturnSuccess()
    {
        //Arrange
        var handler = new CreateReservationHandler(_reservationRepository, _unitOfWork, _guestRepository);
        var command = _fixture.Create<CreateReservationCommand>();

        _reservationRepository.Count(default, Guid.Parse(command.RoomId), command.Arrival, command.Departure).ReturnsForAnyArgs(0);
        _unitOfWork.Commit(Guid.Empty).ReturnsForAnyArgs(Guid.NewGuid());

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task UpdateReservationHandler_IndisponibleRoom_ShouldReturnError()
    {
        //Arrange
        var handler = new UpdateReservationHandler(_reservationRepository, _unitOfWork);
        var command = _fixture.Create<UpdateReservationCommand>();

        _reservationRepository.Count(default, default, command.Arrival, command.Departure).ReturnsForAnyArgs(1);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
    }
    
    [Fact]
    public async Task UpdateReservationHandler_DisponibleRoom_ShouldReturnSuccess()
    {
        //Arrange
        var handler = new UpdateReservationHandler(_reservationRepository, _unitOfWork);
        var command = _fixture.Create<UpdateReservationCommand>();

        _reservationRepository.Count(default, command.RoomId, command.Arrival, command.Departure).ReturnsForAnyArgs(0);
        _unitOfWork.Commit().ReturnsForAnyArgs(true);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}