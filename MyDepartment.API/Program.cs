using Department.API;
using Department.API.Exception;
using Department.Application.Validation;
using Department.Infrastructure.Messaging;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDiDepartmentApi(builder.Configuration);
builder.Services.AddHostedService<DepartmentConsumerFromEmployee>();

builder.Services.AddValidatorsFromAssembly(typeof(CreateDepartmentValidationCommand).Assembly);

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
