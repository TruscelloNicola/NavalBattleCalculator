using IPT71Frontend.DBHandler.Classes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IPT71Frontend.DamageAlgorithmProto
{
    public class AlgoShip: Ship
    {
        public AlgoShip() { }
        public AlgoShip(Ship ship)
        {
            Number = 1;
            Name = ship.Name;
            Type = ship.Type;
            Manpower = ship.Manpower ?? 0;
            SurfaceDetection = ship.SurfaceDetection ?? 0;
            SubDetection = ship.SubDetection ?? 0;
            SubVisibility = ship.SubVisibility ?? 0;
            SurfaceVisibility = ship.SurfaceVisibility ?? 0;
            LightAttack = ship.LightAttack ?? 0;
            LightPiercing = ship.LightPiercing ?? 0;
            HeavyAttack = ship.HeavyAttack ?? 0;
            HeavyPiercing = ship.HeavyPiercing ?? 0;
            TorpedoAttack = ship.TorpedoAttack ?? 0;
            DepthCharges = ship.DepthCharges ?? 0;
            AntiAir = ship.AntiAir ?? 0;
            MaxSpeed = ship.MaxSpeed ?? 0;
            MaxRange = ship.MaxRange ?? 0;
            HP = ship.HP ?? 0;
            Armor = ship.Armor ?? 0;
            Reliability = ship.Reliability ?? 0;
            FuelUsage = ship.FuelUsage ?? 0;
            ProductionCost = ship.ProductionCost ?? 0;
            LightBatteryHitChance = ship.LightBatteryHitChance ?? 0;
            HeavyBatteryHitChance = ship.HeavyBatteryHitChance ?? 0;
            TorpedoHitChance = ship.TorpedoHitChance ?? 0;
            DeckSize = ship.DeckSize ?? 0;
            TorpedoDamageReduction = ship.TorpedoDamageReduction ?? 0;
            IncomingTorpedoCriticalChance = ship.IncomingTorpedoCriticalChance ?? 0;
        }
        public int Number { get; set; }
    }
}
