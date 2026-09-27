using DeskFlow.Api.Data;
using DeskFlow.Api.Repositories;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped<ICategoriasServices, CategoriasServices>();
builder.Services.AddScoped<IChamadosServices, ChamadosServices>();

builder.Services.AddScoped<ICategoriasRespository, CategoriasRepository>();
builder.Services.AddScoped<IChamadosRepository, ChamadosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(op =>
    {
        op.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
