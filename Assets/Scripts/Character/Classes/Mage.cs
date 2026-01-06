using Character.Base;
using Core;

namespace Character.Classes
{
    public class Mage : CharacterBase
    {
        protected override void Awake()
        {
            CharacterClass = CharacterClass.Mage;
            base.Awake();
        }

        protected override void InitializeStats()
        {
            Stats.SetDefaultStats(
                maxHP: 80f,
                maxMP: 200f,
                attack: 30f,
                defense: 8f,
                moveSpeed: 2f
            );

            CharacterName = "Mage";
        }

        protected override bool CanEquip(Equipment.Base.Equipment equipment)
        {
            if (equipment == null)
            {
                return false;
            }

            if ((equipment.EquipmentSlot, equipment.EquipmentType) is
                (EquipmentSlot.Weapon1, not EquipmentType.Staff) or
                (EquipmentSlot.Weapon2, not EquipmentType.Shield))
            {
                return false;
            }

            return equipment.EquipmentType switch
            {
                EquipmentType.Helmet => true,
                EquipmentType.Armor => true,
                EquipmentType.Pants => true,
                EquipmentType.Gloves => true,
                EquipmentType.Boots => true,
                EquipmentType.Staff => true,
                EquipmentType.Shield => true,
                EquipmentType.Ring => true,
                EquipmentType.Necklace => true,
                EquipmentType.Wings => true,
                EquipmentType.Mount => true,
                _ => false
            };
        }

        public float CalculateMagicPower()
        {
            return Stats.GetStat(StatType.Attack);
        }
    }
}