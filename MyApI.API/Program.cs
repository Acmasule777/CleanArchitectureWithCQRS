using FluentValidation;
using MyApI.API;
using MyApI.API.Exceptions;
using MyAPI.Application.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDiApi(builder.Configuration);

builder.Services.AddHttpClient("DepartmentService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5297/");
    
});

builder.Services.AddHttpClient("PayrollService", client =>
{
client.BaseAddress = new Uri("http://localhost:5233/");

});

builder.Services.AddValidatorsFromAssembly(typeof(createEmployeeCommandValidations).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(updateEmployeeValidationCommand).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
