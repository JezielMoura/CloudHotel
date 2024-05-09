using CloudHotel.Domain.ReservationAggregate;
using FluentAssertions;

namespace CloudHotel.Unit.Tests.ReservationAggregate;

public sealed class ReservationTests
{
    private readonly Fixture _fixture;

    public ReservationTests()
    {
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("2020-01-01", "2020-01-05", 4)]
    [InlineData("2020-06-08", "2020-06-10", 2)]
    [InlineData("2020-12-31", "2021-01-09", 9)]
    [InlineData("2020-12-15", "2020-12-30", 15)]
    public void GetNightsNumber_ShouldReturnNightsNumber(string arrival, string departure, int nights)
    {
        //Arrange
        var roomDetails = _fixture.Create<RoomDetails>();
        var arrivalDate = DateOnly.Parse(arrival);
        var departureDate = DateOnly.Parse(departure);
        var reservation = new Reservation(Guid.NewGuid(), arrivalDate, departureDate, 100M, roomDetails);

        //Act
        var result = reservation.GetNightsNumber();

        //Assert
        result.Should().Be(nights);
    }

    [Theory]
    [InlineData("2020-01-01", "2020-01-05", 400, 100)]
    [InlineData("2020-01-01", "2020-01-05", 100, 25)]
    [InlineData("2020-01-01", "2020-01-05", 50, 12.5)]
    [InlineData("2020-01-01", "2020-01-05", 1000, 250)]
    public void GetAveragePricePerNight_ShouldReturnAveragePricePerNight(string arrival, string departure, decimal price, decimal nightPrice)
    {
        //Arrange
        var roomDetails = _fixture.Create<RoomDetails>();
        var arrivalDate = DateOnly.Parse(arrival);
        var departureDate = DateOnly.Parse(departure);
        var reservation = new Reservation(Guid.NewGuid(), arrivalDate, departureDate, price, roomDetails);

        //Act
        var result = reservation.GetAveragePricePerNight();

        //Assert
        result.Should().Be(nightPrice);
    }

    [Fact]
    public void SetGuestDetails_ShouldSetGuestProperty()
    {
        //Arrange
        var roomDetails = _fixture.Create<RoomDetails>();
        var arrivalDate = DateOnly.Parse("2025-01-01");
        var departureDate = DateOnly.Parse("2025-01-03");
        var reservation = new Reservation(Guid.NewGuid(), arrivalDate, departureDate, 100, roomDetails);
        
        //Act
        reservation.SetGuestDetails(Guid.NewGuid(), "GuestName");
        
        //Assert
        reservation.Guest.Should().NotBeNull();
    }
}
