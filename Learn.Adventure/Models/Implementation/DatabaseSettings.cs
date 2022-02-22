using Learn.Adventure.Models.Abstractions;

namespace Learn.Adventure.Models.Implementation
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}