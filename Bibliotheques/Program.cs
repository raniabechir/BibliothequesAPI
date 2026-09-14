using Bibliotheques.Api;
using Bibliotheques.Api.EndPoints;
using Bibliotheques.Core.Interfaces;
using Bibliotheques.Core.Services;
using Bibliotheques.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IServiceBibliotheque, ServiceBibliotheque>();
builder.Services.AddScoped<IDepotBibliotheque, DepotBibliotheques>();

builder.Services.AddExceptionHandler<GestionnaireExceptionsGlobal>();
builder.Services.AddProblemDetails();

var app = builder.Build();

//app.UseHttpsRedirection();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();

app.MapBibliothequeEndpoints();

app.Run();

public partial class Program
{
}