using Blazored.LocalStorage;
using Newtonsoft.Json;
using Sample.BlazorServer.Service;
using System.Net.Http.Headers;
using System.Text;

namespace Sample.BlazorServer.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService localStorageService;

        public BaseRepository(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory=httpClientFactory;
            this.localStorageService = localStorageService;
        }

        public async Task<bool> Create(string url, T entity)
        {
            var request= new HttpRequestMessage(HttpMethod.Post, url);
            if (entity==null)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(entity),Encoding.UTF8,"application/json");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response= await client.SendAsync(request);
            if (response.StatusCode==System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public  async Task<bool> Delete(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+ id);
            if (id <=0)
            {
                return false;
            }
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<IList<T>> GetAll(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);

            }
            return null;
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(string url, int id, T entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url + id);
            if (id <= 0)
            {
                return false;
            }
            request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }
        public async Task<string> GetBearerToken()
        {
            return await localStorageService.GetItemAsync<string>("AuthJwtToken");
        }

        public async Task<T> GetById(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+ id);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);

            }
            return null;
        }
    }

}

