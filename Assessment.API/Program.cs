using Assessment.BLL.Interface;
using Assessment.BLL;
using Assessment.DAL.Interface;
using Assessment.DAL;
using Assessment.Helpers;
using Assessment.Helpers.Interface;
using Assessment.DTO.DataObjects;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Helpers
builder.Services.AddScoped<IJWTHelper, JWTHelper>();
builder.Services.AddScoped<IDapperHelper, DapperHelper>();
builder.Services.AddSingleton<RepositoryFactory, RepositoryFactory>();
builder.Services.AddSingleton<ConnectionStringFactory, ConnectionStringFactory>();
//BLL
builder.Services.AddScoped<IUserBLL, UserBLL>();
//DAL
builder.Services.AddScoped<IUserDAL, UserDAL>();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSettingsConfig>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "GlobalCORSPolicy",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["AppSettings:CORSOrigin"]);
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
});

//auth is setup to validate against jwt tokens that are sent in the header of a request
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),

    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["Token"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors("GlobalCORSPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
