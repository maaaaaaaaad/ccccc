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
        [SerializeField] private float animationSpeed = 5f;

        private CharacterBase _character;
        private float _targetFillAmount;
        private float _currentFillAmount;
        private float _displayedMP;
        private float _targetMP;
        private float _maxMP;

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

            var currentMP = _character.Stats.GetStat(StatType.CurrentMP);
            _maxMP = _character.Stats.GetStat(StatType.MaxMP);

            _targetMP = currentMP;
            _displayedMP = currentMP;
            _targetFillAmount = _maxMP > 0 ? currentMP / _maxMP : 0f;
            _currentFillAmount = _targetFillAmount;

            ApplyFillAmount(_currentFillAmount);
            ApplyText(_displayedMP, _maxMP);
        }

        private void OnStatChanged(StatType statType, float oldValue, float newValue)
        {
            if (statType == StatType.MaxMP)
            {
                _maxMP = newValue;
                UpdateTargetValues();
                return;
            }

            if (statType != StatType.CurrentMP) return;

            _targetMP = newValue;
            UpdateTargetValues();
        }

        private void UpdateTargetValues()
        {
            _targetFillAmount = _maxMP > 0 ? _targetMP / _maxMP : 0f;
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
            if (mpText == null) return;
            if (Mathf.Approximately(_displayedMP, _targetMP)) return;

            _displayedMP = Mathf.Lerp(_displayedMP, _targetMP, Time.deltaTime * animationSpeed);

            if (Mathf.Abs(_displayedMP - _targetMP) < 0.5f)
            {
                _displayedMP = _targetMP;
            }

            ApplyText(_displayedMP, _maxMP);
        }

        private void ApplyFillAmount(float amount)
        {
            if (fillImage == null) return;

            fillImage.fillAmount = amount;
        }

        private void ApplyText(float currentMP, float maxMP)
        {
            if (mpText == null) return;

            mpText.text = $"{Mathf.FloorToInt(currentMP)}/{Mathf.FloorToInt(maxMP)}";
        }
    }
}
