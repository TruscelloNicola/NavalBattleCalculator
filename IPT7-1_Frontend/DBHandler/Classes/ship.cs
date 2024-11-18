using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace IPT71Frontend.DBHandler.Classes
{
    public class Ship
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Manpower { get; set; }
        public int? SurfaceDetection { get; set; }
        public int? SubDetection { get; set; }
        public int? SubVisibility { get; set; }
        public int? SurfaceVisibility { get; set; }
        public double? LightAttack { get; set; }
        public double? LightPiercing { get; set; }
        public double? HeavyAttack { get; set; }
        public double? HeavyPiercing { get; set; }
        public int? TorpedoAttack { get; set; }
        public int? DepthCharges { get; set; }
        public double? AntiAir { get; set; }
        public double? MaxSpeed { get; set; }
        public int? MaxRange { get; set; }
        public double? HP { get; set; }
        public double? Armor { get; set; }
        public int? Reliability { get; set; }
        public int? FuelUsage { get; set; }
        public double? ProductionCost { get; set; }
        public double? LightBatteryHitChance { get; set; }
        public double? HeavyBatteryHitChance { get; set; }
        public double? TorpedoHitChance { get; set; }
        public double? DeckSize { get; set; }
        public double? TorpedoDamageReduction { get; set; }
        public double? IncomingTorpedoCriticalChance { get; set; }
    }
}
