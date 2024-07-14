var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration for CompanyConnection
builder.Services.Configure<YourNamespace.Models.CompanyConnection>(builder.Configuration.GetSection("CompanyConnection"));

// Register the DatabaseConnectionHelper and services
builder.Services.AddScoped<YourNamespace.Helpers.DatabaseConnectionHelper>();
builder.Services.AddScoped<YourNamespace.Services.ICustomerService, YourNamespace.Services.CustomerService>();
builder.Services.AddScoped<YourNamespace.Services.IPaymentService, YourNamespace.Services.PaymentService>();
builder.Services.AddScoped<YourNamespace.Services.IItemService, YourNamespace.Services.ItemService>();
builder.Services.AddScoped<YourNamespace.Services.IStockService, YourNamespace.Services.StockService>();
builder.Services.AddScoped<YourNamespace.Services.IPriceListService, YourNamespace.Services.PriceListService>();
builder.Services.AddScoped<YourNamespace.Services.ISapB1Service, YourNamespace.Services.SapB1Service > ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
