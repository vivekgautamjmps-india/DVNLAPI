using DVNLAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "DVNLAPI",
        Version = "v1",
        Description = "Simple sample Web API built on .NET 8"
    });
});

// Register in-memory data service as singleton so data persists for app lifetime
builder.Services.AddSingleton<IItemService, InMemoryItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DVNLAPI v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Simple health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", utc = DateTime.UtcNow }));

app.Run();
