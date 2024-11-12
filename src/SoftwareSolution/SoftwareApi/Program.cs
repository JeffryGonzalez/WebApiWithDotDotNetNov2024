using Marten;
using Software.Api.Catalog;
using Software.Api.Configuration;


var builder = WebApplication.CreateBuilder(args);



builder.AddCustomFeatureManagement();

builder.Services.AddCustomServices();
builder.Services.AddCustomOasGeneration();

builder.Services.AddControllers();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("IsSoftwareCenter", policy =>
    {
        policy.RequireRole("software-center");
    });

var connectionString = builder.Configuration.GetConnectionString("software") ??
    throw new Exception("No Connection String");

builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);
}).UseLightweightSessions();


builder.Services.AddScoped<CatalogManager>(); // 99% of the time you want "Scoped"
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }