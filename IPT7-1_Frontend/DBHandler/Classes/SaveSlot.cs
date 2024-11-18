using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IPT7_1_Frontend.DBHandler.Classes
{
    public class SaveSlot
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        public string SaveDate { get; set; }
        public string SaveName { get; set; }

        public List<SaveContainsShip> SaveContainsShips { get; set; }
    }
}
