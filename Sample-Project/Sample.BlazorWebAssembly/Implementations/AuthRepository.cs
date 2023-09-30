using Blazored.LocalStorage;
using Newtonsoft.Json;
using Sample.BlazorWebAssembly.AuthProvider;
using Sample.BlazorWebAssembly.DTO;
using Sample.BlazorWebAssembly.Endpoints;
using Sample.BlazorWebAssembly.Service;
using System.Net.Http.Headers;
using System.Text;

namespace Sample.BlazorWebAssembly.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationProvider _authenticationProvider;
        public AuthRepository(AuthenticationProvider authenticationProvider, ILocalStorageService localStorageService, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
            _authenticationProvider=authenticationProvider;
        }

        public async Task<bool> Login(LoginDTO dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, StaticEndpoints.AuthLoginEndpoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")
            };
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            var Apiresponse = JsonConvert.DeserializeObject<ResponseDto>(content);
            await _localStorageService.SetItemAsync("AuthJwtToken", Apiresponse.TokenString);
            await _localStorageService.SetItemAsync("Email", Apiresponse.Email);
            // Change auth state of app
            await ((AuthenticationProvider)_authenticationProvider).LoggedIn(Apiresponse.Email);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", Apiresponse.TokenString);
            return true;

        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("AuthJwtToken");
            // Change auth state of app
            await ((AuthenticationProvider)_authenticationProvider).LoggedOut();
           
        }

        public async Task<bool> Register(UserDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, StaticEndpoints.AuthRegisterEndpoint)
            {
                Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")
            };
            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;

        }
    }
}
