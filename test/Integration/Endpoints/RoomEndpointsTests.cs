using CloudHotel.Application.Rooms.CreateRoom;
using CloudHotel.Application.Rooms.SearchRoom;
using CloudHotel.Application.Rooms.UpdateRoom;

namespace CloudHotel.Integration.Tests.Endpoints;

public class RoomEndpointsTests : IClassFixture<CloudHotelApiFixture>
{
    private readonly HttpClient _client;
    private readonly CloudHotelApiFixture  _factory;
    private readonly Fixture _fixture;

    public RoomEndpointsTests(CloudHotelApiFixture factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(3, 10));
        _fixture.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString()[..10]));
    }

    [Fact]
    public async Task Get_ShouldReturnOk()
    {
        //Arrange
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();
        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand);

        //Act
        var response = await _client.GetAsync("/api/rooms?page=1&limit=1");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Add_ValidRequest_ShouldReturnOk()
    {
        //Arrange
        var command = _fixture.Create<CreateRoomCommand>();

        //Act
        var response = await _client.PostAsJsonAsync("/api/rooms", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("valid name", "", "")]
    [InlineData("", "valid description", "")]
    [InlineData("", "", "valid code")]
    public async Task Add_InvalidRequest_ShouldReturnBadRequest(string name, string description, string code)
    {
        //Arrange
        var command = new CreateRoomCommand(name, description, code);

        //Act
        var response = await _client.PostAsJsonAsync("/api/rooms", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ValidRequest_ShouldReturnOK()
    {
        //Arrange
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();
        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand);
        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?page=1&limit=1");
        var room = getResponse!.First().Rooms.First();
        var command = new UpdateRoomCommand(room.Id, "new name", "new description", "new code");

        //Act
        var response = await _client.PutAsJsonAsync("/api/rooms", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("valid name", "", "")]
    [InlineData("", "valid description", "")]
    [InlineData("", "", "valid code")]
    public async Task Update_InvalidRequest_ShouldReturnBadRequest(string name, string description, string code)
    {
        //Arrange
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();
        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand);
        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomResponse>>("/api/rooms?page=1&limit=1");
        var roomId = getResponse!.First().Id;
        var command = new UpdateRoomCommand(roomId, name, description, code);

        //Act
        var response = await _client.PutAsJsonAsync("/api/rooms", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ShouldReturnOK()
    {
        //Arrange
        var createRoomCommand = _fixture.Create<CreateRoomCommand>();
        await _client.PostAsJsonAsync("/api/rooms", createRoomCommand);
        var getResponse = await _client.GetFromJsonAsync<IEnumerable<SearchRoomGroupResponse>>("/api/rooms?page=1&limit=1");
        var room = getResponse!.First().Rooms.First();

        //Act
        var response = await _client.DeleteAsync($"/api/rooms/{room.Id}");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}