namespace Learn.Adventure.Models.Abstractions
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }
        string Host { get; set; }
        string Port { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string ConnectionString { get; }
    }
}