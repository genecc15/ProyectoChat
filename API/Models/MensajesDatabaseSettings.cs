namespace API.Models
{
    public class MensajesDatabaseSettings : IMensajesDatabaseSettings
    {
        public string MensajesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMensajesDatabaseSettings
    {
        string MensajesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
