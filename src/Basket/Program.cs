using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using RabbitMQ.Client;
using Basket.Repositories;
using Basket.Repositories.Interfaces;
using Basket.Entities;
using Basket.Events;
using Polly;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration["ConnectionStrings:RedisConnection"];
    return ConnectionMultiplexer.Connect(connectionString);
});

//Set up rabbitMQ connection
builder.Services.AddSingleton<IConnection>(sp =>
{
    var logger = sp.GetRequiredService<ILogger<Program>>(); 
    //Retry for server startup
    var retryPolicy = Policy
        .Handle<BrokerUnreachableException>()
        .Or<SocketException>()
        .WaitAndRetry(new[]
        {
            TimeSpan.FromSeconds(3),
            TimeSpan.FromSeconds(7),
            TimeSpan.FromSeconds(15)
        }, 
        (exception, timeSpan) =>{
            logger.LogWarning($"Failed to connect to RabbitMQ. Retrying in {timeSpan.TotalSeconds} seconds. Exception: {exception.Message}");
        });

    return retryPolicy.Execute(() =>
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq" , DispatchConsumersAsync = true}; 
        return factory.CreateConnection();
    });
});

//register Repository
builder.Services.AddScoped<IBasketRepository, RedisBasketRepository>();
builder.Services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();
builder.Services.AddHostedService<RabbitMQConsumerHostedService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseRouting(); 
app.MapControllers(); 

app.Run();


