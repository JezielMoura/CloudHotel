using CloudHotel.Application.Guests.CreateGuest;

namespace CloudHotel.Integration.Tests.Endpoints;

public class GuestEndpointsTests : IClassFixture<CloudHotelApiFixture>
{
    private readonly HttpClient _client;
    private readonly CloudHotelApiFixture  _factory;
    private readonly Fixture _fixture;

    public GuestEndpointsTests(CloudHotelApiFixture factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 3));
        _fixture.Customizations.Add(new StringGenerator(() => Guid.NewGuid().ToString()[..10]));
    }

    [Fact]
    public async Task Get_ShouldReturnOk()
    {
        //Arrange
        var createGuestCommand = _fixture.Create<CreateGuestCommand>();
        await _client.PostAsJsonAsync("/api/Guests", createGuestCommand);

        //Act
        var response = await _client.GetAsync("/api/Guests?Page=1&Limit=1");
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Add_ValidRequest_ShouldReturnOk()
    {
        //Arrange
        var command = _fixture.Create<CreateGuestCommand>();

        //Act
        var response = await _client.PostAsJsonAsync("/api/guests", command);
    
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}