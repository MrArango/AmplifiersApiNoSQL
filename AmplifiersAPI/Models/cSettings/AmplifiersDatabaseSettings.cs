namespace AmplifiersAPI.Models
{
    public class AmplifiersDatabaseSettings : IAmplifiersDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string AmplifiersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAmplifiersDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string AmplifiersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


    }
}