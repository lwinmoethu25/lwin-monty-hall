
using HealthChecks.UI.Client;
using LwinMontyHall.Extensions;
using LwinMontyHall.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var corsPolicyName = "AllowAll";
var builder = WebApplication.CreateBuilder(args);

// Custom healthcheck example
builder.Services.AddHealthChecks()
    .AddGCInfoCheck("GCInfo");

// Write healthcheck custom results to healthchecks-ui (use InMemory for the DB - AspNetCore.HealthChecks.UI.InMemory.Storage nuget package)
builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

// Add services to the container.
 // configure DI for application services
builder.Services.AddScoped<IMontyHallService, MontyHallService>();

builder.Services.AddCorsConfig(corsPolicyName);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Brotli/Gzip response compression (prod only)
builder.Services.AddResponseCompressionConfig(builder.Configuration);

// Register the Swagger services (using OpenApi 3.0)
builder.Services.AddOpenApiDocument(settings =>
{
    settings.Version = "v1";
    settings.Title = "Monty Hall API";
    settings.Description = "Lwin Assignment";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseCors(corsPolicyName);

// Show/write HealthReport data from healthchecks (AspNetCore.HealthChecks.UI.Client nuget package)
app.UseHealthChecksUI();
app.UseHealthChecks("/healthchecks-json", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Register the Swagger generator and the Swagger UI middlewares
// NSwage.MsBuild + adding automation config in GhostUI.csproj makes this part of the build step (updates to API will be handled automatically)
app.UseOpenApi();
app.UseSwaggerUi3();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
