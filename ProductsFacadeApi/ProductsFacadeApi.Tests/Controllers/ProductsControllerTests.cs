using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ProductsFacadeApi.Authorization.Contexts;
using ProductsFacadeApi.Controllers;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Domain.Entities;
using ProductsFacadeApi.Infrastructure.Dto;
using ProductsFacadeApi.Tests.Core;
using Shouldly;
using Xunit;

namespace ProductsFacadeApi.Tests.Controllers
{
    [Collection("ServicesFixture")]
    public class ProductsControllerTests : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ServiceProvider _provider;
        private readonly ProductsController _productsController;
        private readonly IProductsRepository _productsRepository;
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;

        public ProductsControllerTests(ServicesFixture fixture)
        {
            _httpMessageHandler = fixture.HttpMessageHandler;
            _provider = fixture.Services.BuildServiceProvider();
            
            _unitOfWork = _provider.GetService<UnitOfWork>();
            _productsController = _provider.GetService<ProductsController>();
            _productsRepository = _provider.GetService<IProductsRepository>();
        }

        [Fact]
        public async void GetProductsPage_ShouldReturnSuccess()
        {
            // Arrange
            _unitOfWork.Begin();
            var userId = Guid.NewGuid();
            var product = new Product()
            {
                Title = "Test Product",
                Description = "This is a test product.",
                Price = 1000
            };
            
            // Action

                await _productsRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();


            SetCurrentUserId(userId);
            var productsPageResult = await _productsController.GetProducts(new PageDto()
            {
                PageIndex = 1,
                PageSize = 10,
            });

            // Assert
            productsPageResult.Result.ShouldBeOfType<OkObjectResult>();
        }
        
        private void SetCurrentUserId(Guid id)
        {
            _httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"users/{id}")),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new UserDto { Id = id }), 
                Encoding.UTF8, 
                "application/json")
            });

            AuthorizationContext.CurrentUserId = id;
        }
        
        public void Dispose()
        {
        }
    }
}