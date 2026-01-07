using Character.Base;
using UI.HUD;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CharacterBase playerCharacter;
        [SerializeField] private HUDManager hudManager;

        [Header("Test Settings")]
        [SerializeField] private float testDamageAmount = 20f;
        [SerializeField] private float testHealAmount = 15f;
        [SerializeField] private float testManaUseAmount = 10f;
        [SerializeField] private float testManaRestoreAmount = 8f;
        [SerializeField] private float testExpGainAmount = 25f;

        private void Start()
        {
            InitializeGame();
        }

        private void Update()
        {
            HandleTestInput();
        }

        private void InitializeGame()
        {
            if (playerCharacter == null)
            {
                playerCharacter = FindFirstObjectByType<CharacterBase>();
            }

            if (playerCharacter == null)
            {
                Debug.LogWarning("GameManager: Player character not found!");
                return;
            }

            if (hudManager == null)
            {
                hudManager = FindFirstObjectByType<HUDManager>();
            }

            if (hudManager == null)
            {
                Debug.LogWarning("GameManager: HUDManager not found!");
                return;
            }

            hudManager.Initialize(playerCharacter);
            Debug.Log($"GameManager: Initialized with player '{playerCharacter.CharacterName}'");
        }

        private void HandleTestInput()
        {
            if (playerCharacter == null) return;

            if (Input.GetKeyDown(KeyCode.T))
            {
                TestDamage();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                TestHeal();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                TestUseMana();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                TestRestoreMana();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                TestGainExp();
            }
        }

        private void TestDamage()
        {
            playerCharacter.Stats.ModifyCurrentStat(StatType.CurrentHP, -testDamageAmount);
            var currentHP = playerCharacter.Stats.GetStat(StatType.CurrentHP);
            Debug.Log($"[Test] Took {testDamageAmount} damage. Current HP: {currentHP}");
        }

        private void TestHeal()
        {
            playerCharacter.Stats.ModifyCurrentStat(StatType.CurrentHP, testHealAmount);
            var currentHP = playerCharacter.Stats.GetStat(StatType.CurrentHP);
            Debug.Log($"[Test] Healed {testHealAmount}. Current HP: {currentHP}");
        }

        private void TestUseMana()
        {
            playerCharacter.Stats.ModifyCurrentStat(StatType.CurrentMP, -testManaUseAmount);
            var currentMP = playerCharacter.Stats.GetStat(StatType.CurrentMP);
            Debug.Log($"[Test] Used {testManaUseAmount} mana. Current MP: {currentMP}");
        }

        private void TestRestoreMana()
        {
            playerCharacter.Stats.ModifyCurrentStat(StatType.CurrentMP, testManaRestoreAmount);
            var currentMP = playerCharacter.Stats.GetStat(StatType.CurrentMP);
            Debug.Log($"[Test] Restored {testManaRestoreAmount} mana. Current MP: {currentMP}");
        }

        private void TestGainExp()
        {
            playerCharacter.Stats.ModifyCurrentStat(StatType.Experience, testExpGainAmount);
            var currentExp = playerCharacter.Stats.GetStat(StatType.Experience);
            var maxExp = playerCharacter.Stats.GetStat(StatType.MaxExperience);
            Debug.Log($"[Test] Gained {testExpGainAmount} EXP. Current: {currentExp}/{maxExp}");
        }
    }
}
