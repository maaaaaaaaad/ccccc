using Character.Base;
using Core;

namespace Character.Classes
{
    public class Knight : CharacterBase
    {
        protected override void Awake()
        {
            CharacterClass = CharacterClass.Knight;
            base.Awake();
        }

        protected override void InitializeStats()
        {
            Stats.SetDefaultStats(
                150f,
                50f,
                25f,
                15f,
                2f
            );

            CharacterName = "Knight";
        }

        protected override bool CanEquip(Equipment.Base.Equipment equipment)
        {
            if (equipment == null) return false;

            return equipment.EquipmentType switch
            {
                EquipmentType.Helmet => true,
                EquipmentType.Armor => true,
                EquipmentType.Pants => true,
                EquipmentType.Gloves => true,
                EquipmentType.Boots => true,
                EquipmentType.Sword => true,
                EquipmentType.Shield => true,
                EquipmentType.Ring => true,
                EquipmentType.Necklace => true,
                EquipmentType.Wings => true,
                EquipmentType.Mount => true,
                _ => false
            };
        }

        private bool IsDualWielding()
        {
            var weapon1 = GetEquippedItem(EquipmentSlot.Weapon1);
            var weapon2 = GetEquippedItem(EquipmentSlot.Weapon2);

            if (weapon1 == null || weapon2 == null) return false;

            return weapon1.EquipmentType == EquipmentType.Sword &&
                   weapon2.EquipmentType == EquipmentType.Sword;
        }

        public float CalculateAttackPower()
        {
            var baseAttack = Stats.GetStat(StatType.Attack);

            if (!IsDualWielding()) return baseAttack;

            return baseAttack * 1.5f;
        }
    }
}