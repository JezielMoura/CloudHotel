using CloudHotel.Application.Abstractions.Models;
using CloudHotel.Application.Guests.CreateGuest;
using CloudHotel.Application.Guests.SearchGuest;
using CloudHotel.Application.Reservations.CreateReservation;
using CloudHotel.Application.Reservations.UpdateReservation;
using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Application.Rooms.SearchRoom;

namespace CloudHotel.Integration.Tests.Endpoints;

public class ReservationEndpointsTests : IClassFixture<CloudHotelApiFixture>
{
    private readonly HttpClient _client;
    private readonly CloudHotelApiFixture  _factory;
    private readonly Fixture _fixture;
    private readonly DateRange _dateRange = new (DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(90)));

    public ReservationEndpointsTests(CloudHotelApiFixture factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(3, 5));
        _fixture.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString()[..10]));
        _fixture.Register(() => DateOnly.Parse("2025-01-01"));
    }

    [Fact]
    public async Task Get_ReservationExists_ShouldReturnOK()
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(_fixture.Create<int>());
        var createCommand = _fixture.Create<CreateReservationCommand>();
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();

        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand with { Quantity = 1});

        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?Page=1&Limit=1");
        var room = getResponse!.First().Rooms.First();

        createCommand = createCommand with { Arrival = arrival, Departure = departure, RoomCode = room.Code, RoomId = room.Id.ToString(), Price = Random.Shared.Next(100, 1500) };

        var createResponse = await _client.PostAsJsonAsync("/api/reservations", createCommand);
        var reservationId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        //Act
        var response = await _client.GetAsync($"/api/reservations/{reservationId}");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_ReservationNotExists_ShouldReturnNotFound()
    {
        //Arrange
        var reservationId = Guid.NewGuid();

        //Act
        var response = await _client.GetAsync($"/api/reservations/{reservationId}");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Search_ShouldReturnOk()
    {
        //Act
        var response = await _client.GetAsync("/api/reservations?ArrivalFrom=2000-01-01&ArrivalTo=2030-01-01");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Add_ValidRequest_ShouldReturnOk()
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(_fixture.Create<int>());
        var createCommand = _fixture.Create<CreateReservationCommand>();
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();

        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand with { Quantity = 1});

        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?Page=1&Limit=1");
        var room = getResponse!.First().Rooms.First();

        createCommand = createCommand with { Arrival = arrival, Departure = departure, RoomCode = room.Code, RoomId = room.Id.ToString(), Price = Random.Shared.Next(100, 1500) };

        //Act
        var response = await _client.PostAsJsonAsync("/api/reservations", createCommand);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("c93d6070-36ea-4987-be31-cad55e23ad2f", "")]
    public async Task Add_InvalidRequest_ShouldReturnBadRequest(string roomId, string guestName)
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(_fixture.Create<int>());
        var command = new CreateReservationCommand(arrival, departure, 100, roomId, "roomCode", guestName, "mail", "phone", "doc", "type", "", "", "", "", "");

        //Act
        var response = await _client.PostAsJsonAsync("/api/reservations", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ValidRequest_ShouldReturnOk()
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(_fixture.Create<int>());
        var createCommand = _fixture.Create<CreateReservationCommand>();
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();

        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand with { Quantity = 1 });

        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?Page=1&Limit=1");
        var room = getResponse!.First().Rooms.First();

        createCommand = createCommand with { Arrival = arrival, Departure = departure, RoomCode = room.Code, RoomId = room.Id.ToString()};

        var createResponse = await _client.PostAsJsonAsync("/api/reservations", createCommand);
        var reservationId = await createResponse.Content.ReadFromJsonAsync<Guid>();
        var createGuestCommand = _fixture.Create<CreateGuestCommand>();

        await _client.PostAsJsonAsync("/api/Guests", createGuestCommand);
        
        var guestList = await _client.GetFromJsonAsync<IEnumerable<SearchGuestResponse>>("/api/Guests?Page=1&Limit=1");
        var updateCommand = _fixture.Create<UpdateReservationCommand>();

        updateCommand = updateCommand with { Id = reservationId, Status = 1, Arrival = arrival.AddDays(30), Departure = departure.AddDays(30), RoomId = room.Id, GuestId = guestList!.First().Id, Price = Random.Shared.Next(100, 1500)};

        //Act
        var response = await _client.PutAsJsonAsync("/api/reservations", updateCommand);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("c93d6070-36ea-4987-be31-cad55e23ad2f", "")]
    public async Task Update_InvalidRequest_ShouldReturnBadRequest(string roomId, string guestName)
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(_fixture.Create<int>());
        var command = new CreateReservationCommand(arrival, departure, 100, roomId, "", guestName, "mail", "phone", "doc", "type", "", "", "", "", "");

        //Act
        var response = await _client.PostAsJsonAsync("/api/reservations", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ShouldReturnOK()
    {
        //Arrange
        var arrival = _dateRange.Dates[Random.Shared.Next(0, 90)];
        var departure = arrival.AddDays(1);
        var createCommand = _fixture.Create<CreateReservationCommand>();
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();

        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand with { Quantity = 1});

        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?Page=1&Limit=1");
        var room = getResponse!.First().Rooms.First();

        createCommand = createCommand with { Arrival = arrival, Departure = departure, RoomCode = room.Code, RoomId = room.Id.ToString(), Price = Random.Shared.Next(100, 1500) };

        var createResponse = await _client.PostAsJsonAsync("/api/reservations", createCommand);
        var reservationId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        //Act
        var response = await _client.DeleteAsync($"/api/reservations/{reservationId}");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}