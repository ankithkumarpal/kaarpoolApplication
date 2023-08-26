using Interfaces;
using CarPool.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using System.Text;
using DataLayer;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CarPoolContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("CarpoolDBConnectionString")));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOfferRideService , OfferRideService>();
builder.Services.AddScoped<IBookRideService , BookRideService>();
builder.Services.AddScoped<ITokenGeneration , TokenGenerationService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IBookRideRepository, BookRideRepository>();
builder.Services.AddScoped<IOfferRideRepository , OfferRideRepository>();
builder.Services.AddSingleton<ILog, Logger>();
builder.Services.AddAutoMapper(typeof(AutoMapperService));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// allowing only specific client to access 
var MyAllowSpecificOrigins = "";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000" ,
                               "https://kaarpool.azurewebsites.net")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

