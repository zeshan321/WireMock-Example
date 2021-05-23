using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace IntegrationTestsAfter
{
    public class IntegrationTestFactory<T> : WebApplicationFactory<T> where T : class
    {
        private const bool Mock = true;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (!Mock) return;

            var wireMockService = WireMockServer.Start();

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("LuckyNumberUrl", wireMockService.Urls[0])
                });
            }).ConfigureServices(collection => collection.AddSingleton(wireMockService));
        }
    }
}