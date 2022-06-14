using Console_Kafka_Consumer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{    
    services.AddHostedService<ConsumerService>();        
}).Build();


await host.RunAsync();