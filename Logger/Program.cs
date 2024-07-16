using Logger.Consumers;
using Logger.Consumers.Product;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<ProductCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(false));
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.UseMessageRetry(opt =>
        {
            opt.Interval(5, TimeSpan.FromSeconds(2));
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();