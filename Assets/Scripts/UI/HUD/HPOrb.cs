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
        [SerializeField] private float animationSpeed = 5f;

        private CharacterBase _character;
        private float _targetFillAmount;
        private float _currentFillAmount;
        private float _displayedHP;
        private float _targetHP;
        private float _maxHP;

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

            var currentHP = _character.Stats.GetStat(StatType.CurrentHP);
            _maxHP = _character.Stats.GetStat(StatType.MaxHP);

            _targetHP = currentHP;
            _displayedHP = currentHP;
            _targetFillAmount = _maxHP > 0 ? currentHP / _maxHP : 0f;
            _currentFillAmount = _targetFillAmount;

            ApplyFillAmount(_currentFillAmount);
            ApplyText(_displayedHP, _maxHP);
        }

        private void OnStatChanged(StatType statType, float oldValue, float newValue)
        {
            if (statType == StatType.MaxHP)
            {
                _maxHP = newValue;
                UpdateTargetValues();
                return;
            }

            if (statType != StatType.CurrentHP) return;

            _targetHP = newValue;
            UpdateTargetValues();
        }

        private void UpdateTargetValues()
        {
            _targetFillAmount = _maxHP > 0 ? _targetHP / _maxHP : 0f;
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
            if (hpText == null) return;
            if (Mathf.Approximately(_displayedHP, _targetHP)) return;

            _displayedHP = Mathf.Lerp(_displayedHP, _targetHP, Time.deltaTime * animationSpeed);

            if (Mathf.Abs(_displayedHP - _targetHP) < 0.5f)
            {
                _displayedHP = _targetHP;
            }

            ApplyText(_displayedHP, _maxHP);
        }

        private void ApplyFillAmount(float amount)
        {
            if (fillImage == null) return;

            fillImage.fillAmount = amount;
        }

        private void ApplyText(float currentHP, float maxHP)
        {
            if (hpText == null) return;

            hpText.text = $"{Mathf.FloorToInt(currentHP)}/{Mathf.FloorToInt(maxHP)}";
        }
    }
}
