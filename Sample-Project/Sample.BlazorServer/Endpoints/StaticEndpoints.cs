namespace Sample.BlazorServer.Endpoints
{
    public static class StaticEndpoints
    {
        public static string BaseUrl = "https://localhost:7182/";
        public static string AuthRegisterEndpoint = $"{BaseUrl}api/Account/UserRegister";
        public static string AuthLoginEndpoint = $"{BaseUrl}api/Account/UserLogin";
        public static string GetProductsEndpoint = $"{BaseUrl}api/Products/GetProducts/";
        public static string GetSingleProductsEndpoint = $"{BaseUrl}api/Products/";
        public static string AddProductEndpoint = $"{BaseUrl}api/Products/AddProduct";
        public static string EditProductEndpoint = $"{BaseUrl}api/Products/";
        public static string DeleteProductEndpoint = $"{BaseUrl}api/Products/";
    }
}
