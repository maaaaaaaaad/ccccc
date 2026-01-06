using Character.Base;
using Core;

namespace Character.Classes
{
    public class Archer : CharacterBase
    {
        protected override void Awake()
        {
            CharacterClass = CharacterClass.Archer;
            base.Awake();
        }

        protected override void InitializeStats()
        {
            Stats.SetDefaultStats(
                maxHP: 100f,
                maxMP: 80f,
                attack: 28f,
                defense: 10f,
                moveSpeed: 2f
            );

            CharacterName = "Archer";
        }

        protected override bool CanEquip(Equipment.Base.Equipment equipment)
        {
            if (equipment == null)
            {
                return false;
            }

            if ((equipment.EquipmentSlot, equipment.EquipmentType) is
                (EquipmentSlot.Weapon1, not EquipmentType.Bow) or
                (EquipmentSlot.Weapon2, not EquipmentType.Quiver))
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
                EquipmentType.Bow => true,
                EquipmentType.Quiver => true,
                EquipmentType.Ring => true,
                EquipmentType.Necklace => true,
                EquipmentType.Wings => true,
                EquipmentType.Mount => true,
                _ => false
            };
        }

        public float CalculateRangedAttackPower()
        {
            return Stats.GetStat(StatType.Attack);
        }
    }
}
