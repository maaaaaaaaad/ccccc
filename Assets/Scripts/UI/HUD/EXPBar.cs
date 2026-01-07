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
        [SerializeField] private float animationSpeed = 5f;

        private CharacterBase _character;
        private float _targetFillAmount;
        private float _currentFillAmount;
        private float _displayedEXP;
        private float _targetEXP;
        private float _maxEXP;

        private void Awake()
        {
            if (fillImage != null) fillImage.fillAmount = 0f;
        }

        private void Update()
        {
            AnimateFill();
            AnimateText();
        }

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
            InitializeDisplay();
        }

        private void InitializeDisplay()
        {
            if (_character == null) return;

            var currentEXP = _character.Stats.GetStat(StatType.Experience);
            _maxEXP = _character.Stats.GetStat(StatType.MaxExperience);

            _targetEXP = currentEXP;
            _displayedEXP = 0f;
            _targetFillAmount = _maxEXP > 0 ? currentEXP / _maxEXP : 0f;
            _currentFillAmount = 0f;

            ApplyFillAmount(_currentFillAmount);
            ApplyText(_displayedEXP, _maxEXP);
        }

        private void OnStatChanged(StatType statType, float oldValue, float newValue)
        {
            if (statType == StatType.MaxExperience)
            {
                _maxEXP = newValue;
                UpdateTargetValues();
                return;
            }

            if (statType != StatType.Experience) return;

            _targetEXP = newValue;
            UpdateTargetValues();
        }

        private void UpdateTargetValues()
        {
            _targetFillAmount = _maxEXP > 0 ? _targetEXP / _maxEXP : 0f;
        }

        private void AnimateFill()
        {
            if (fillImage == null) return;
            if (Mathf.Approximately(_currentFillAmount, _targetFillAmount)) return;

            _currentFillAmount = Mathf.Lerp(_currentFillAmount, _targetFillAmount, Time.deltaTime * animationSpeed);

            if (Mathf.Abs(_currentFillAmount - _targetFillAmount) < 0.001f)
            {
                _currentFillAmount = _targetFillAmount;
            }

            ApplyFillAmount(_currentFillAmount);
        }

        private void AnimateText()
        {
            if (expText == null) return;
            if (Mathf.Approximately(_displayedEXP, _targetEXP)) return;

            _displayedEXP = Mathf.Lerp(_displayedEXP, _targetEXP, Time.deltaTime * animationSpeed);

            if (Mathf.Abs(_displayedEXP - _targetEXP) < 0.5f)
            {
                _displayedEXP = _targetEXP;
            }

            ApplyText(_displayedEXP, _maxEXP);
        }

        private void ApplyFillAmount(float amount)
        {
            if (fillImage == null) return;

            fillImage.fillAmount = amount;
        }

        private void ApplyText(float currentEXP, float maxEXP)
        {
            if (expText == null) return;

            var percentage = maxEXP > 0 ? (currentEXP / maxEXP) * 100f : 0f;
            expText.text = $"{Mathf.FloorToInt(currentEXP)}/{Mathf.FloorToInt(maxEXP)} ({percentage:F1}%)";
        }
    }
}
