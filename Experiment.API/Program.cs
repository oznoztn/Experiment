using Experiment.API.Fakes;
using Experiment.Core.Factory;
using Experiment.Core.Repository;
using Experiment.Service;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CalculationStrategyFactory>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IUserRepository, FakeUserRepository>();
builder.Services.AddScoped<IInvoiceRepository, FakeInvoiceRepository>();

builder.Services.AddFluentValidation(s =>
{
    s.RegisterValidatorsFromAssemblyContaining<Program>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
