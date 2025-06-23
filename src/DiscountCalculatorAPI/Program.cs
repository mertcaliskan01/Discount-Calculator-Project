using System.Text.Json.Serialization;
using DiscountCalculatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5080");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register discount strategies
builder.Services.AddScoped<IDiscountStrategy, CategoryDiscountStrategy>();
builder.Services.AddScoped<IDiscountStrategy, CustomerDiscountStrategy>();
builder.Services.AddScoped<IDiscountStrategy, QuantityDiscountStrategy>();

// Register service
builder.Services.AddScoped<DiscountCalculatorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
