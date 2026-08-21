using FluentValidation;
using payroll.API.Exceptions;
using Payroll.API;
using Payroll.Application.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDIAPIPayroll(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(createPayrollValidationCommand).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(updateValidationCommand).Assembly);

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
