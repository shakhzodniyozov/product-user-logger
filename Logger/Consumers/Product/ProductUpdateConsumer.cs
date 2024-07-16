using MassTransit;
using Shared.Events.Contracts.Product;

namespace Logger.Consumers.Product;

public class ProductUpdateConsumer : IConsumer<ProductUpdated>
{
    private static int counter = 0;
    private readonly ILogger<ProductUpdateConsumer> _logger;

    public ProductUpdateConsumer(ILogger<ProductUpdateConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<ProductUpdated> context)
    {
        Console.WriteLine(counter);
        
        if (counter++ < 5)
            throw new Exception("Something went wrong.");
        
        _logger.LogInformation($"Updated product: {context.Message}");
        return Task.CompletedTask;
    }
}