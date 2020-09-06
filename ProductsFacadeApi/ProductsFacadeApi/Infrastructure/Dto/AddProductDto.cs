using Microsoft.AspNetCore.Http;

namespace ProductsFacadeApi.Infrastructure.Dto
{
    /// <summary>
    /// Dto для добавления товара.
    /// </summary>
    public class AddProductDto
    {
        public IFormFile Picture { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}