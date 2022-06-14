namespace Console_Kafka_Consumer
{
    public class ParameterModel
    {
        public ParameterModel()
        {
            BootstrapServers = "localhost:9092";
            GroupId = "Group1";
            Topic = "topic1";
        }

        public string? BootstrapServers { get; set; }
        public string? GroupId { get; set; }
        public string? Topic { get; set; }
    }
}