using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ModernisationChallenge.IntegrationTests.Controllers
{
    public class ControllerTestBase :
        IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly WebApplicationFactory<Program>
            _factory;

        public ControllerTestBase(
            WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateClient(Action<IServiceCollection> registerServices)
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => registerServices(services));
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            return client;
        }
    }
}
