using System.Collections.Generic;
using Core;

namespace Equipment.Base
{
    public class Equipment
    {
        public string Name { get; private set; }
        public EquipmentType EquipmentType { get; private set; }
        public EquipmentSlot EquipmentSlot { get; private set; }
        public int EnhancementLevel { get; private set; }
        public int MaxEnhancementLevel { get; private set; }

        private readonly Dictionary<StatType, float> _baseStats;
        private readonly Dictionary<StatType, float> _enhancementBonusPerLevel;

        public Equipment(string name, EquipmentType equipmentType, EquipmentSlot equipmentSlot,
            int maxEnhancementLevel = 10)
        {
            Name = name;
            EquipmentType = equipmentType;
            EquipmentSlot = equipmentSlot;
            EnhancementLevel = 0;
            MaxEnhancementLevel = maxEnhancementLevel;

            _baseStats = new Dictionary<StatType, float>();
            _enhancementBonusPerLevel = new Dictionary<StatType, float>();
        }

        public void SetBaseStat(StatType statType, float value)
        {
            _baseStats[statType] = value;
        }

        public void SetEnhancementBonus(StatType statType, float bonusPerLevel)
        {
            _enhancementBonusPerLevel[statType] = bonusPerLevel;
        }

        public Dictionary<StatType, float> GetTotalStats()
        {
            var totalStats = new Dictionary<StatType, float>();

            foreach (var (statType, baseValue) in _baseStats)
            {
                var enhancementBonus = _enhancementBonusPerLevel.GetValueOrDefault(statType, 0f);
                var totalValue = baseValue + (enhancementBonus * EnhancementLevel);

                totalStats[statType] = totalValue;
            }

            return totalStats;
        }

        public bool CanEnhance()
        {
            return EnhancementLevel < MaxEnhancementLevel;
        }

        public bool Enhance()
        {
            if (!CanEnhance())
            {
                return false;
            }

            EnhancementLevel++;
            return true;
        }

        public bool Downgrade()
        {
            if (EnhancementLevel <= 0)
            {
                return false;
            }

            EnhancementLevel--;
            return true;
        }

        public float GetStatValue(StatType statType)
        {
            var baseValue = _baseStats.GetValueOrDefault(statType, 0f);
            var enhancementBonus = _enhancementBonusPerLevel.GetValueOrDefault(statType, 0f);

            return baseValue + (enhancementBonus * EnhancementLevel);
        }
    }
}