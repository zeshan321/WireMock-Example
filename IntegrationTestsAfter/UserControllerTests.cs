using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api;
using Api.Repository.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace IntegrationTestsAfter
{
    public class UserControllerTests : IClassFixture<IntegrationTestFactory<Startup>>
    {
        private readonly IntegrationTestFactory<Startup> _factory;
        private readonly WireMockServer? _wireMockServer;

        public UserControllerTests(IntegrationTestFactory<Startup> factory)
        {
            _factory = factory;
            _wireMockServer = _factory.Services.GetService<WireMockServer>();
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

            _wireMockServer?.Given(Request.Create()
                    .WithParam("min", new ExactMatcher("1"))
                    .WithParam("max", new ExactMatcher("100"))
                    .WithParam("count", new ExactMatcher("1")))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBodyAsJson(new List<int> {expectedUser.LuckyNumber}));

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