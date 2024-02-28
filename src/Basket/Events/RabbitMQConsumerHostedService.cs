using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Basket.Events;
using RabbitMQ.Client;

public class RabbitMQConsumerHostedService : IHostedService
{
    private readonly IRabbitMQConsumer _rabbitMQConsumer;
    private readonly ILogger<RabbitMQConsumerHostedService> _logger;
    private readonly IConnection _connection;

    public RabbitMQConsumerHostedService(IRabbitMQConsumer rabbitMQConsumer, IConnection connection, ILogger<RabbitMQConsumerHostedService> logger)
    {
        _rabbitMQConsumer = rabbitMQConsumer;
        _connection = connection;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Consumer Hosted Service is starting.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ Consumer Hosted Service is stopping.");
        // Cleanup
        if (_connection.IsOpen)
        {
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
        return Task.CompletedTask;
    }
}
