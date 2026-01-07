using UnityEngine;

namespace Character.Movement
{
    public class MobileInput : MonoBehaviour
    {
        [SerializeField] private VariableJoystick joystick;

        private bool _hasJoystick;

        public Vector2 MoveInput { get; private set; }
        public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;

        private void Awake()
        {
            _hasJoystick = joystick != null;

            if (!_hasJoystick) Debug.LogWarning("MobileInput: Joystick is not assigned!");
        }

        private void Update()
        {
            ReadJoystickInput();
        }

        private void ReadJoystickInput()
        {
            if (!_hasJoystick)
            {
                MoveInput = Vector2.zero;
                return;
            }

            var horizontal = joystick.Horizontal;
            var vertical = joystick.Vertical;

            MoveInput = new Vector2(horizontal, vertical);

            if (MoveInput.sqrMagnitude > 1f) MoveInput.Normalize();
        }

        public void SetJoystick(VariableJoystick newJoystick)
        {
            joystick = newJoystick;
            _hasJoystick = joystick != null;
        }
    }
}