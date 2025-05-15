using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.UnitOfWork;
//using ProductService.Kafka;
using ProductApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
void RegisterServices(IServiceCollection services)
{
    var assemblies = new[] { Assembly.GetExecutingAssembly() };

    foreach (var type in assemblies.SelectMany(a => a.GetTypes()))
    {
        if (type.IsClass && !type.IsAbstract && type.Name.EndsWith("Repository"))
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
            if (interfaceType != null)
                services.AddScoped(interfaceType, type);
        }
        if (type.IsClass && !type.IsAbstract && type.Name.EndsWith("Service"))
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
            if (interfaceType != null)
                services.AddScoped(interfaceType, type);
        }
    }
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// add connection string
var connection = builder.Configuration.GetConnectionString("ProductConnection");
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connection));

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<ProductDbContext>(options => options.UseLazyLoadingProxies(false).UseSqlServer(connectionString));

//DI Repository,Service
//repo
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//unitofwork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//service
//builder.Services.AddScoped<IProductService, ProductService.Services.ProductService>();

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
