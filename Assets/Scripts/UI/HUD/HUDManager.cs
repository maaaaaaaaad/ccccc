using Character.Base;
using UnityEngine;

namespace UI.HUD
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private HPOrb hpOrb;
        [SerializeField] private MPOrb mpOrb;
        [SerializeField] private EXPBar expBar;

        private CharacterBase _playerCharacter;

        public void Initialize(CharacterBase playerCharacter)
        {
            _playerCharacter = playerCharacter;

            if (_playerCharacter == null) return;

            InitializeHPOrb();
            InitializeMPOrb();
            InitializeEXPBar();
        }

        private void InitializeHPOrb()
        {
            if (hpOrb == null) return;

            hpOrb.Initialize(_playerCharacter);
        }

        private void InitializeMPOrb()
        {
            if (mpOrb == null) return;

            mpOrb.Initialize(_playerCharacter);
        }

        private void InitializeEXPBar()
        {
            if (expBar == null) return;

            expBar.Initialize(_playerCharacter);
        }
    }
}