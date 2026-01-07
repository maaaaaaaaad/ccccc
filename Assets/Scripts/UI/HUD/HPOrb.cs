using Character.Base;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HPOrb : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI hpText;

        private CharacterBase _character;

        private void OnDestroy()
        {
            if (_character != null) _character.Stats.OnStatChanged -= OnStatChanged;
        }

        public void Initialize(CharacterBase character)
        {
            if (_character != null) _character.Stats.OnStatChanged -= OnStatChanged;

            _character = character;

            if (_character == null) return;

            _character.Stats.OnStatChanged += OnStatChanged;
            UpdateDisplay();
        }

        private void OnStatChanged(StatType statType, float oldValue, float newValue)
        {
            if (statType != StatType.CurrentHP && statType != StatType.MaxHP) return;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_character == null) return;

            var currentHP = _character.Stats.GetStat(StatType.CurrentHP);
            var maxHP = _character.Stats.GetStat(StatType.MaxHP);

            UpdateFillAmount(currentHP, maxHP);
            UpdateText(currentHP, maxHP);
        }

        private void UpdateFillAmount(float currentHP, float maxHP)
        {
            if (fillImage == null) return;

            if (maxHP <= 0)
            {
                fillImage.fillAmount = 0f;
                return;
            }

            fillImage.fillAmount = currentHP / maxHP;
        }

        private void UpdateText(float currentHP, float maxHP)
        {
            if (hpText == null) return;

            hpText.text = $"{Mathf.FloorToInt(currentHP)}/{Mathf.FloorToInt(maxHP)}";
        }
    }
}