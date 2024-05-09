using System.Diagnostics.CodeAnalysis;
using CloudHotel.Domain.ReservationAggregate;
using FluentAssertions;

namespace CloudHotel.Unit.Tests.ReservationAggregate;

[ExcludeFromCodeCoverage]
public sealed class ReservationStatusTests
{
    [Fact]
    public void GetAll_ShouldReturnList()
    {
        //Act
        var result = ReservationStatus.GetAll();

        //Assert
        result.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void FromNumber_WithValidNumber_ShouldReturnInstance(int value)
    {
        //Act
        var result = ReservationStatus.FromNumber(value);

        //Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void FromNumber_WithInvalidNumber_ShouldThrowException(int value)
    {
        //Act
        Action action = () => ReservationStatus.FromNumber(value);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData(nameof(ReservationStatus.Pendent))]
    [InlineData(nameof(ReservationStatus.Confirmed))]
    [InlineData(nameof(ReservationStatus.InHouse))]
    [InlineData(nameof(ReservationStatus.Completed))]
    [InlineData(nameof(ReservationStatus.Canceled))]
    public void FromName_WithValidName_ShouldReturnInstance(string name)
    {
        //Act
        var result = ReservationStatus.FromName(name);

        //Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData("InvalidName")]
    [InlineData("OtherInvalidName")]
    [InlineData("SomeInvalidName")]
    public void FromName_WithInvalidName_ShouldThrowException(string name)
    {
        //Act
        Action action = () => ReservationStatus.FromName(name);

        //Assert
        action.Should().Throw<InvalidOperationException>();
    }
}