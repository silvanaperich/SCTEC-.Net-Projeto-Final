using DeskFlow.Api.Data;
using DeskFlow.Api.DTOs.Erros;
using DeskFlow.Api.Middlewares;
using DeskFlow.Api.Repositories;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi( op => 
{
    //configuração especifica para o Swagger funcionar com o token Bearer
    op.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();
        doc.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "Identity access token",
            In = ParameterLocation.Header,
            Description = "Cole apenas o accessToken retornado por /auth/login. O Swagger adiciona 'Bearer' automaticamente."
        };
        return Task.CompletedTask;
    });

    op.AddOperationTransformer((operation, context, _) =>
    {
        if (context.Description.ActionDescriptor.EndpointMetadata.Any(metadata => metadata is IAuthorizeData))
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", context.Document, null)] = []
            });
        }

        return Task.CompletedTask;
    });
});

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = ctx =>
        {
            var erros = ctx.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            bool erroDeJson = erros.Any(m =>
                string.IsNullOrWhiteSpace(m) ||
                m.Contains("JSON") ||
                m.Contains("Path:") ||
                m.Contains("field is required"));

            string mensagem = erroDeJson
                ? "Corpo da requisição inválido. Verifique o formato do JSON e os valores informados."
                : string.Join(" ", erros);

            return new BadRequestObjectResult(new ErrorResponseDTO(mensagem));
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorization();

builder.Services.AddScoped<ICategoriasServices, CategoriasServices>();
builder.Services.AddScoped<IChamadosServices, ChamadosServices>();
builder.Services.AddScoped<IInteracoesServices, InteracoesServices>();

builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<IChamadosRepository, ChamadosRepository>();
builder.Services.AddScoped<IInteracoesRepository, InteracoesRepository>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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

app.MapGroup("/auth").MapIdentityApi<IdentityUser>();

app.Run();
