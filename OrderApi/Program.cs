//using Microsoft.EntityFrameworkCore;
//using OrderService.Kafka;
//using OrderService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add service entity framework
var connectionString = builder.Configuration.GetConnectionString("OrderConnection");
//builder.Services.AddDbContext<OrderDbContext>(options =>
//{
//    options
//    .UseLazyLoadingProxies(false)
//    .UseSqlServer(connectionString);
//});

// add middleware controllers
builder.Services.AddControllers();

// bat cors
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

// add producer kafka
//builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
