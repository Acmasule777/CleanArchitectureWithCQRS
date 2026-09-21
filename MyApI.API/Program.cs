using Employee.Infrastructure.Messaging;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyApI.API;
using MyApI.API.Exceptions;
using MyAPI.Application.Validation;
using MyAPI.Infrastructure.messanger;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDiApi(builder.Configuration);

builder.Services.AddScoped<RabbitMQPublisher>();
builder.Services.AddHostedService<FromDepartmentNameGetIdConsumer>();

//builder.Services.AddHttpClient("DepartmentService", client => { client.BaseAddress = new Uri("http://departmentservice:5051/"); });
//builder.Services.AddHttpClient("PayrollService", client => { client.BaseAddress = new Uri("http://payrollservice:5052/"); });

builder.Services.AddHttpClient("DepartmentService", client => { client.BaseAddress = new Uri("http://localhost:5297/"); });
builder.Services.AddHttpClient("PayrollService", client => { client.BaseAddress = new Uri("http://localhost:5233/"); });

builder.Services.AddValidatorsFromAssembly(typeof(createEmployeeCommandValidations).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(updateEmployeeValidationCommand).Assembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "EmployeeService:";
});

//That below configuration for JWT Authentication

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
