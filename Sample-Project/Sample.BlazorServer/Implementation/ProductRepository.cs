using Blazored.LocalStorage;
using Sample.BlazorServer.DTO;
using Sample.BlazorServer.Service;

namespace Sample.BlazorServer.Implementation
{
    public class ProductRepository : BaseRepository<ProductDTO>, IProductRepository
    {
        public ProductRepository(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService) : base(httpClientFactory, localStorageService)
        {
        }
    }
}
