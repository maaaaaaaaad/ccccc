using Character.Base;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class EXPBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI expText;

        private CharacterBase _character;

        public void Initialize(CharacterBase character)
        {
            if (_character != null)
            {
                _character.Stats.OnStatChanged -= OnStatChanged;
            }

            _character = character;

            if (_character == null)
            {
                return;
            }

            _character.Stats.OnStatChanged += OnStatChanged;
            UpdateDisplay();
        }

        private void OnDestroy()
        {
            if (_character != null)
            {
                _character.Stats.OnStatChanged -= OnStatChanged;
            }
        }

        private void OnStatChanged(StatType statType, float oldValue, float newValue)
        {
            if (statType != StatType.Experience && statType != StatType.MaxExperience)
            {
                return;
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_character == null)
            {
                return;
            }

            var currentEXP = _character.Stats.GetStat(StatType.Experience);
            var maxEXP = _character.Stats.GetStat(StatType.MaxExperience);

            UpdateFillAmount(currentEXP, maxEXP);
            UpdateText(currentEXP, maxEXP);
        }

        private void UpdateFillAmount(float currentEXP, float maxEXP)
        {
            if (fillImage == null)
            {
                return;
            }

            if (maxEXP <= 0)
            {
                fillImage.fillAmount = 0f;
                return;
            }

            fillImage.fillAmount = currentEXP / maxEXP;
        }

        private void UpdateText(float currentEXP, float maxEXP)
        {
            if (expText == null)
            {
                return;
            }

            var percentage = maxEXP > 0 ? (currentEXP / maxEXP) * 100f : 0f;
            expText.text = $"{Mathf.FloorToInt(currentEXP)}/{Mathf.FloorToInt(maxEXP)} ({percentage:F1}%)";
        }
    }
}