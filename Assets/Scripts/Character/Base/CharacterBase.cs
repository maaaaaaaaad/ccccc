using System.Collections.Generic;
using Core;
using Equipment.Base;
using Stats;
using UnityEngine;

namespace Character.Base
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public string CharacterName { get; protected set; }
        public CharacterClass CharacterClass { get; protected set; }
        public CharacterStats Stats { get; private set; }

        private readonly Dictionary<EquipmentSlot, Equipment.Base.Equipment> _equippedItems = new();

        protected virtual void Awake()
        {
            Stats = new CharacterStats();
            InitializeStats();
        }

        protected abstract void InitializeStats();

        public bool EquipItem(Equipment.Base.Equipment equipment)
        {
            if (!CanEquip(equipment))
            {
                return false;
            }

            if (_equippedItems.ContainsKey(equipment.EquipmentSlot))
            {
                UnequipItem(equipment.EquipmentSlot);
            }

            _equippedItems[equipment.EquipmentSlot] = equipment;
            UpdateStatsFromEquipment();
            return true;
        }

        public bool UnequipItem(EquipmentSlot slot)
        {
            var removed = _equippedItems.Remove(slot);
            if (removed)
            {
                UpdateStatsFromEquipment();
            }

            return removed;
        }

        public Equipment.Base.Equipment GetEquippedItem(EquipmentSlot slot)
        {
            return _equippedItems.GetValueOrDefault(slot);
        }

        public Dictionary<EquipmentSlot, Equipment.Base.Equipment> GetAllEquippedItems()
        {
            return new Dictionary<EquipmentSlot, Equipment.Base.Equipment>(_equippedItems);
        }

        protected abstract bool CanEquip(Equipment.Base.Equipment equipment);

        private void UpdateStatsFromEquipment()
        {
            foreach (StatType statType in System.Enum.GetValues(typeof(StatType)))
            {
                Stats.SetEquipmentBonus(statType, 0f);
            }

            foreach (var (_, equipment) in _equippedItems)
            {
                var equipmentStats = equipment.GetTotalStats();

                foreach (var (statType, value) in equipmentStats)
                {
                    var currentBonus = Stats.GetStat(statType);
                    Stats.SetEquipmentBonus(statType, currentBonus + value);
                }
            }
        }

        public float GetStat(StatType statType)
        {
            return Stats.GetStat(statType);
        }

        public void TakeDamage(float damage)
        {
            var actualDamage = CalculateDamageReduction(damage);
            Stats.ModifyCurrentStat(StatType.CurrentHP, -actualDamage);

            if (Stats.GetStat(StatType.CurrentHP) <= 0)
            {
                Die();
            }
        }

        private float CalculateDamageReduction(float damage)
        {
            var defense = Stats.GetStat(StatType.Defense);
            var defensePercentage = Stats.GetStat(StatType.DefensePercentage);

            var damageAfterDefense = Mathf.Max(0, damage - defense);
            var finalDamage = damageAfterDefense * (1f - defensePercentage / 100f);

            return Mathf.Max(0, finalDamage);
        }

        protected virtual void Die()
        {
            Debug.Log($"{CharacterName} has died!");
        }
    }
}