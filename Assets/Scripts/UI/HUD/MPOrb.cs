using Character.Base;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class MPOrb : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI mpText;

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
            if (statType != StatType.CurrentMP && statType != StatType.MaxMP) return;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_character == null) return;

            var currentMP = _character.Stats.GetStat(StatType.CurrentMP);
            var maxMP = _character.Stats.GetStat(StatType.MaxMP);

            UpdateFillAmount(currentMP, maxMP);
            UpdateText(currentMP, maxMP);
        }

        private void UpdateFillAmount(float currentMP, float maxMP)
        {
            if (fillImage == null) return;

            if (maxMP <= 0)
            {
                fillImage.fillAmount = 0f;
                return;
            }

            fillImage.fillAmount = currentMP / maxMP;
        }

        private void UpdateText(float currentMP, float maxMP)
        {
            if (mpText == null) return;

            mpText.text = $"{Mathf.FloorToInt(currentMP)}/{Mathf.FloorToInt(maxMP)}";
        }
    }
}