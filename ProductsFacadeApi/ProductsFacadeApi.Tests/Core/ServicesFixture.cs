using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ProductsFacadeApi.Controllers;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Infrastructure.Options;
using UserManagerService.Client;
using UserManagerService.Client.Options;
using Xunit;

namespace ProductsFacadeApi.Tests.Core
{
    public class ServicesFixture
    {
        public Mock<UserManagerServiceClient> UserManagerServiceClientMock
        {
            get;
            private set;
        }

        public Mock<HttpMessageHandler> HttpMessageHandler
        {
            get;
            private set;
        }

        public ServiceCollection Services { get; }

        public ServicesFixture()
        {
            var serviceCollection = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            new Startup(config).ConfigureServices(serviceCollection);

            MockUserManagerServiceClient();
            serviceCollection.AddSingleton<IConfiguration>(config);
            serviceCollection.AddScoped<ProductsController>();
            serviceCollection.AddScoped<UnitOfWork>();
            serviceCollection.AddSingleton(UserManagerServiceClientMock.Object);
            serviceCollection.AddSingleton(MockHttpFactory());

            Program.InitializeDataBase(serviceCollection.BuildServiceProvider());

            Services = serviceCollection;
        }

        private IHttpClientFactory MockHttpFactory()
        {
            var content = new
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Value = new { data = new { message = "Test message" } }
            };

            HttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        JsonConvert.SerializeObject(content), 
                        Encoding.UTF8, 
                        "application/json")
                });

            var httpClient = new HttpClient(HttpMessageHandler.Object) { BaseAddress = new Uri("https://www.test.test/") };
            var httpFactoryMock = new Mock<IHttpClientFactory>();
            httpFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            return httpFactoryMock.Object;
        }

        private void MockUserManagerServiceClient()
        {
            var options = new OptionsWrapper<UserManagerServiceOptions>( new UserManagerServiceOptions () );
            UserManagerServiceClientMock = new Mock<UserManagerServiceClient>(
                MockHttpFactory(),
                options);
        }

        [CollectionDefinition("ServicesFixture")]
        public class TestFixtureCollection : ICollectionFixture<ServicesFixture>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }
    }
}
