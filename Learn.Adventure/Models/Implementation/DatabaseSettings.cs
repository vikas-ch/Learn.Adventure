using Learn.Adventure.Models.Abstractions;

namespace Learn.Adventure.Models.Implementation
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString => $"mongodb://{User}:{Password}@{Host}:{Port}";
    }
}