namespace AmplifiersAPI.Models
{
    public class AppsSettings : IAppsSettings
    {
        public string Secret { get; set; }
    }

    public interface IAppsSettings
    {
        string Secret { get; set; }
    }
}
