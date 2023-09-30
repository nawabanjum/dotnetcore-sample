using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Sample.BlazorServer.AuthProvider;
using Sample.BlazorServer.Data;
using Sample.BlazorServer.Implementation;
using Sample.BlazorServer.Service;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUpload, Upload>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
    p.GetRequiredService<AuthenticationProvider>());
builder.Services.AddScoped<JwtSecurityTokenHandler>();


builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//});
//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//             {
//                 googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//                 googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//             });
//.AddGoogle("Google", options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//})
//.AddFacebook("Facebook", options =>
//{
//    options.ClientId = builder.Configuration["Authentication:Facebook:AppId"];
//    options.ClientSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
//});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
