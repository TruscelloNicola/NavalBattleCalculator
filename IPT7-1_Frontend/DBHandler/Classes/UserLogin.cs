using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IPT7_1_Frontend.DBHandler.Classes
{
    public class UserLogin
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public ObjectId SaveSlotId { get; set; }
        public SaveSlot SaveSlot { get; set; }
    }
}
