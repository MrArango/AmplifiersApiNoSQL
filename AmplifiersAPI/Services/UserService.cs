using AmplifiersAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace AmplifiersAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Users> _users;

        private readonly string _secret;

        public UserService(IAmplifiersDatabaseSettings databaseSettings, IAppsSettings app)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(databaseSettings.ConnectionString));
            settings.RetryWrites = false;

            settings.SslSettings =
            new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _users = database.GetCollection<Users>(databaseSettings.UsersCollectionName);

            _secret = app.Secret;
        }

        public List<Users> Get() =>
            _users.Find(user => true).ToList();

        public Users GetById(string id) =>
            _users.Find<Users>(user => user.Id == id).FirstOrDefault();

        public void Create(Users user) =>
            _users.InsertOne(user);

        public void UpdateById(string id, Users userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void DeleteById(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public UserResponse Login(Users user)
        {
            UserResponse Uresponse = new UserResponse();
            
            var SearchedUser = _users.Find(U =>
                    U.Username == user.Username && U.Password == user.Password).FirstOrDefault();

            if (SearchedUser != null)
            {
                Uresponse.Username = SearchedUser.Username;
                Uresponse.Token = Tools.Tools.GenerateToken(SearchedUser, _secret);

                return Uresponse;
            }
            else
            {
                return null;
            }
        }
    }
}
