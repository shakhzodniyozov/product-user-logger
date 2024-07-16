using MassTransit;
using Shared.Events.Contracts.Product;

namespace Logger.Consumers.Product;

public class ProductCreatedConsumer : IConsumer<ProductCreated>
{
    private readonly ILogger _logger;

    public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreated> context)
    {
        _logger.LogInformation($"Created product: {context.Message}");

        return Task.CompletedTask;
    }
}