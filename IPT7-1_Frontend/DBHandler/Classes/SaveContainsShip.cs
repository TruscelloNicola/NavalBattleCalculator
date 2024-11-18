using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using IPT71Frontend.DBHandler.Classes;

namespace IPT7_1_Frontend.DBHandler.Classes
{
    public class SaveContainsShip
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        public ObjectId SaveSlotId { get; set; }
        public ObjectId ShipId { get; set; }
        public int Amount { get; set; }
        public string Side { get; set; }

        public Ship _ship { get; set; }
        public SaveSlot _saveSlot { get; set; }
    }
}
