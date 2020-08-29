using AmplifiersAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace AmplifiersAPI.Services
{
    public class AmplifierService
    {
        private readonly IMongoCollection<Amplifiers> _amplifiers;

        public AmplifierService(IAmplifiersDatabaseSettings databaseSettings, IAppsSettings app)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(databaseSettings.ConnectionString));
            settings.RetryWrites = false;

            settings.SslSettings =
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _amplifiers = database.GetCollection<Amplifiers>(databaseSettings.AmplifiersCollectionName);
        }

        //cambiar por id
        public List<Amplifiers> Get(string id) =>
            _amplifiers.Find(amp => amp.User == id).ToList();

        public Amplifiers GetById(string id) =>
            _amplifiers.Find<Amplifiers>(amp => amp.Id == id).FirstOrDefault();

        public void Create(Amplifiers amp) =>
            _amplifiers.InsertOne(amp);

        public void UpdateById(string id, Amplifiers ampIn) =>
            _amplifiers.ReplaceOne(amp => amp.Id == id, ampIn);

        public void DeleteById(string id) =>
            _amplifiers.DeleteOne(amp => amp.Id == id);
    }
}
