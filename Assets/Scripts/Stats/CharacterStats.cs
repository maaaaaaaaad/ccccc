using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Stats
{
    public class CharacterStats
    {
        private readonly Dictionary<StatType, float> _baseStats;
        private readonly Dictionary<StatType, float> _equipmentBonuses;

        private readonly Dictionary<StatType, float> _stats;

        public CharacterStats()
        {
            _stats = new Dictionary<StatType, float>();
            _baseStats = new Dictionary<StatType, float>();
            _equipmentBonuses = new Dictionary<StatType, float>();

            InitializeStats();
        }

        public event Action<StatType, float, float> OnStatChanged;

        public float GetStat(StatType type)
        {
            return _stats.GetValueOrDefault(type, 0f);
        }

        public void SetBaseStat(StatType type, float value)
        {
            _baseStats[type] = value;
            RecalculateStat(type);
        }

        public void SetEquipmentBonus(StatType type, float bonus)
        {
            _equipmentBonuses[type] = bonus;
            RecalculateStat(type);
        }

        public void ModifyCurrentStat(StatType type, float amount)
        {
            if (!_stats.ContainsKey(type)) return;

            var oldValue = _stats[type];
            var newValue = Mathf.Clamp(_stats[type] + amount, 0f, GetMaxValue(type));
            _stats[type] = newValue;

            if (Math.Abs(oldValue - newValue) > 0.001f) OnStatChanged?.Invoke(type, oldValue, newValue);
        }

        private void RecalculateStat(StatType type)
        {
            var baseValue = _baseStats.GetValueOrDefault(type, 0f);
            var bonusValue = _equipmentBonuses.GetValueOrDefault(type, 0f);
            var oldValue = _stats.GetValueOrDefault(type, 0f);
            var newValue = baseValue + bonusValue;

            _stats[type] = newValue;

            if (Math.Abs(oldValue - newValue) > 0.001f) OnStatChanged?.Invoke(type, oldValue, newValue);
        }

        private float GetMaxValue(StatType type)
        {
            return type switch
            {
                StatType.CurrentHP => GetStat(StatType.MaxHP),
                StatType.CurrentMP => GetStat(StatType.MaxMP),
                StatType.Experience => GetStat(StatType.MaxExperience),
                _ => float.MaxValue
            };
        }

        private void InitializeStats()
        {
            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                _stats[statType] = 0f;
                _baseStats[statType] = 0f;
                _equipmentBonuses[statType] = 0f;
            }
        }

        public void SetDefaultStats(float maxHP, float maxMP, float attack, float defense, float moveSpeed = 2f)
        {
            SetBaseStat(StatType.MaxHP, maxHP);
            SetBaseStat(StatType.CurrentHP, maxHP);
            SetBaseStat(StatType.MaxMP, maxMP);
            SetBaseStat(StatType.CurrentMP, maxMP);
            SetBaseStat(StatType.Attack, attack);
            SetBaseStat(StatType.Defense, defense);
            SetBaseStat(StatType.MoveSpeed, moveSpeed);
            SetBaseStat(StatType.AttackSpeed, 1f);
            SetBaseStat(StatType.DefenseSuccessRate, 0f);
            SetBaseStat(StatType.DefensePercentage, 0f);
            SetBaseStat(StatType.Level, 1f);
            SetBaseStat(StatType.Experience, 0f);
            SetBaseStat(StatType.MaxExperience, 100f);
        }
    }
}