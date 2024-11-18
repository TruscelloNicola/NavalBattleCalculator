using IPT71Frontend.DBHandler.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPT71Frontend.DamageAlgorithmProto
{
    public static class BattleSimulator
    {
        public static BattleOutcome SimulateBattle(List<AlgoShip> sideAShips, List<AlgoShip> sideBShips)
        {
            var bestCaseSideA = SimulateSingleBattle(sideAShips, sideBShips, isBestCase: true);
            var worstCaseSideA = SimulateSingleBattle(sideAShips, sideBShips, isBestCase: false);

            var bestCaseSideB = SimulateSingleBattle(sideBShips, sideAShips, isBestCase: true);
            var worstCaseSideB = SimulateSingleBattle(sideBShips, sideAShips, isBestCase: false);

            return new BattleOutcome
            {
                BestCaseSideA = bestCaseSideA,
                WorstCaseSideA = worstCaseSideA,
                BestCaseSideB = bestCaseSideB,
                WorstCaseSideB = worstCaseSideB
            };
        }

        private static BattleResult SimulateSingleBattle(List<AlgoShip> attackingShips, List<AlgoShip> defendingShips, bool isBestCase)
        {
            int initialAttackingShips = attackingShips.Sum(s => s.Number);
            int initialDefendingShips = defendingShips.Sum(s => s.Number);

            float attackingFirepower = CalculateFirepower(attackingShips);
            float defendingFirepower = CalculateFirepower(defendingShips);

            float attackingDamage = CalculateDamage(attackingFirepower, defendingShips, isBestCase);
            float defendingDamage = CalculateDamage(defendingFirepower, attackingShips, isBestCase);

            int survivingAttackingShips = CalculateSurvivingShips(initialAttackingShips, defendingDamage, attackingShips.Average(s => (float)s.HP));
            int survivingDefendingShips = CalculateSurvivingShips(initialDefendingShips, attackingDamage, defendingShips.Average(s => (float)s.HP));

            int totalManpowerLostAttacking = CalculateManpowerLost(attackingShips, survivingAttackingShips);
            int totalManpowerLostDefending = CalculateManpowerLost(defendingShips, survivingDefendingShips);

            return new BattleResult
            {
                SurvivingShips = survivingAttackingShips,
                TotalManpowerLost = totalManpowerLostAttacking,
                TotalShipsLost = initialAttackingShips - survivingAttackingShips
            };
        }

        private static float CalculateFirepower(List<AlgoShip> ships)
        {
            return (float)ships.Sum(s => s.Number * ((double)s.HeavyAttack + (double)s.LightAttack + (double)s.TorpedoAttack + (double)s.AntiAir));
        }

        private static float CalculateDamage(float firepower, List<AlgoShip> targetShips, bool isBestCase)
        {
            float totalHP = (float)targetShips.Sum(s => (double)s.HP * s.Number);
            float totalArmor = (float)targetShips.Sum(s => (double)s.Armor * s.Number);

            if (isBestCase)
            {
                // Minimize losses for best case
                return Convert.ToSingle(firepower * targetShips.Average(s => (double)s.Reliability) * (1 - totalArmor / totalHP));
            }
            else
            {
                // Maximize losses for worst case
                return Convert.ToSingle(firepower * targetShips.Average(s => (double)s.Reliability) * (1 - totalArmor / totalHP));
            }
        }

        private static int CalculateSurvivingShips(int initialShips, float damage, float averageHP)
        {
            int shipsLost = (int)(damage / averageHP);
            return Math.Max(0, initialShips - shipsLost);
        }

        private static int CalculateManpowerLost(List<AlgoShip> ships, int survivingShips)
        {
            int initialManpower = (int)ships.Sum(s => s.Manpower * s.Number);
            int survivingManpower = (int)(survivingShips / (float)ships.Sum(s => s.Number) * initialManpower);
            return initialManpower - survivingManpower;
        }

        public static AlgoShip CalculateShipAverage(List<AlgoShip> ships)
        {
            return new AlgoShip
            {
                Number = ships.Sum(s => s.Number),
                Manpower = (int)ships.Average(s => s.Manpower),
                HP = ships.Average(s => s.HP),
                Armor = ships.Average(s => s.Armor),
                MaxSpeed = ships.Average(s => s.MaxSpeed),
                Reliability = (int)ships.Average(s => s.Reliability),
                FuelUsage = (int)ships.Average(s => s.FuelUsage),
                SurfaceVisibility = Convert.ToInt32(ships.Average(s => s.SurfaceVisibility)),
                SubVisibility = Convert.ToInt32(ships.Average(s => s.SubVisibility)),
                LightAttack = ships.Average(s => s.LightAttack),
                LightPiercing = ships.Average(s => s.LightPiercing),
                HeavyAttack = ships.Average(s => s.HeavyAttack),
                HeavyPiercing = ships.Average(s => s.HeavyPiercing),
                TorpedoAttack = (int)ships.Average(s => s.TorpedoAttack),
                AntiAir = ships.Average(s => s.AntiAir),
                DeckSize = ships.Average(s => s.DeckSize)
            };
        }
    }

    public class BattleOutcome
    {

        public BattleOutcome()
        {
            BestCaseSideA = new BattleResult();
            WorstCaseSideA = new BattleResult();
            BestCaseSideB = new BattleResult();
            WorstCaseSideB = new BattleResult();
        }
        public BattleResult BestCaseSideA { get; set; }
        public BattleResult WorstCaseSideA { get; set; }
        public BattleResult BestCaseSideB { get; set; }
        public BattleResult WorstCaseSideB { get; set; }
    }

    public class BattleResult
    {
        public BattleResult()
        {
            SurvivingShips = 0;
            TotalManpowerLost = 0;
            TotalShipsLost = 0;
        }
        public int SurvivingShips { get; set; }
        public int TotalManpowerLost { get; set; }
        public int TotalShipsLost { get; set; }
    }

}
