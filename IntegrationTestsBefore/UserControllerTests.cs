using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api;
using Api.Repository.Models;
using FluentAssertions;
using Xunit;

namespace IntegrationTestsBefore
{
    public class UserControllerTests : IClassFixture<IntegrationTestFactory<Startup>>
    {
        private readonly IntegrationTestFactory<Startup> _factory;

        public UserControllerTests(IntegrationTestFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var expectedUser = new User
            {
                Id = 2,
                Name = "Trudy Campbell",
                LuckyNumber = 22
            };

            var httpClient = _factory.CreateClient();

            // Act
            var responseMessage = await httpClient.GetAsync($"/users/{expectedUser.Id}");

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            var response = await responseMessage.Content.ReadFromJsonAsync<User>();
            response.Should().BeEquivalentTo(expectedUser);
        }
    }
}