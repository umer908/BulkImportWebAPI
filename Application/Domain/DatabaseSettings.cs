namespace Application.Domain
{
    public class DatabaseSettings
    {
        public string Server { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }

    public class FileStorageSettings
    {
        public string BatchPath { get; set; }
    }
}
