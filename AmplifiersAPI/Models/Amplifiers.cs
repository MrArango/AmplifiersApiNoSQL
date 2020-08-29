using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AmplifiersAPI.Models
{
    public class Amplifiers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ampname")]
        public string Ampname { get; set; }
        
        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("user")]
        public string User { get; set; }

        public string Description { get; set; }
        
        
    }
}
