using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sample.WebApi.Configuration;
using Sample.WebApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
//// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<App_BlazorDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<UserPofile>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<App_BlazorDBContext>();
//Register AutoMapper Service
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(option =>
{
    //When authenticate make sure you are using
    //beer scheme, mean anybody who is attempting
    //to access anything that we are secured
    //must provide jwt bearer
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //    Mean you are going to challenge according
    //to the JWT bearer
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;



})
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,  //ensure that issuer is valid issuer
            ValidateLifetime = true,//ensure that token is not expire
            ClockSkew = TimeSpan.Zero,//is timespan zero,that is used to difference in times b / w two computers
            ValidIssuer = builder.Configuration["jWTSetting:Issuer"],
            ValidAudience = builder.Configuration["jWTSetting:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jWTSetting:Key"]))
        };
    });
builder.Services.AddAuthentication()
           .AddGoogle(options =>
           {
               options.ClientId = "360531172431-k8nskke7vm4ifum1la4ersdmu6n46t0b.apps.googleusercontent.com";
               options.ClientSecret = "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";
           });
//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = "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";
//    googleOptions.ClientSecret = "360531172431-k8nskke7vm4ifum1la4ersdmu6n46t0b.apps.googleusercontent.com";
//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();
app.UseCors("corspolicy");
app.MapControllers();

app.Run();
