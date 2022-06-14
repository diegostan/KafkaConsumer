using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Console_Kafka_Consumer.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConsumerConfig _config;
        private readonly ILogger<ConsumerService> _logger;
        private readonly ParameterModel _parameters;
        
        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _parameters = new ParameterModel(); 
            _config = new ConsumerConfig()
            {
                BootstrapServers = _parameters.BootstrapServers,
                GroupId = _parameters.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };    

            _logger.LogInformation($"BootstrapServers: {_parameters.BootstrapServers} - GroupId: {_parameters.GroupId} - Topic: {_parameters.Topic}");

            _consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando novas mensagens...");
            _consumer.Subscribe(_parameters.Topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"{_parameters.GroupId} mensagem: {result.Message.Value} - {DateTime.Now}");               
                    
                });
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            _logger.LogInformation("Conexão finalizada");
            return Task.CompletedTask;
        }

    }
}