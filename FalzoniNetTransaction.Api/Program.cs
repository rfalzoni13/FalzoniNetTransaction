using FalzoniNetTransaction.Api.Records;
using FalzoniNetTransaction.Service;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

ILogger logger = LoggerFactory.Create(builder =>  builder.AddConsole()).CreateLogger("Program");

builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var listErrors = context.ModelState.Select(x =>
            {
                return new ValidationError(x.Key, x.Value!.Errors.Select(y => y.ErrorMessage).Distinct());
            });

            logger.LogError(JsonSerializer.Serialize(listErrors, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

            return new ContentResult
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Falzoni Transactions .NET 8",
            Description = "Api de demonstração de transações com C# e .NET 8",
            Version = "v1"
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

builder.Services.AddSingleton<TransactionService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyOrigin()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.MapHealthChecks("/api/Health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        await context.Response.WriteAsJsonAsync(new StatusHealth("API Funcionando!"));
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "{documentName}/swagger.json";
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/v1/swagger.json", "Falzoni Transactions .NET 8");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();

/// <summary>
/// Classe Program (inicialização da aplicação).
/// </summary>
public partial class Program;