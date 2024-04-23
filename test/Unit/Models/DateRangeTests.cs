using CloudHotel.Application.Abstractions.Models;
using FluentAssertions;

namespace CloudHotel.Unit.Tests.Models;

public sealed class DateRangeTests
{
    [Theory]
    [InlineData("2025-01-07", "2025-01-13", 7)]
    [InlineData("2025-01-01", "2025-01-31", 31)]
    [InlineData("2025-01-01", "2025-12-31", 365)]
    public void Constructor_TwoDates_ShouldSetDays(string from, string to, int days)
    {
        //Act
        var dateRage = new DateRange(DateOnly.Parse(from), DateOnly.Parse(to));

        //Assert
        dateRage.Days.Should().Be(days);
    }

    [Fact]
    public void Constructor_TwoDates_ShouldRetunDays()
    {
        //Arrange
        var from = "2030-12-10";
        var to = "2030-12-15";

        //Act
        var dateRage = new DateRange(DateOnly.Parse(from), DateOnly.Parse(to));

        //Assert
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-10"));
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-11"));
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-12"));
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-13"));
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-14"));
        dateRage.Dates.Should().Contain(DateOnly.Parse("2030-12-15"));
    }
}