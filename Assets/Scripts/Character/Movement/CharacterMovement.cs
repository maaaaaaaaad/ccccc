using Character.Base;
using Core;
using UnityEngine;

namespace Character.Movement
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MobileInput))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        private CharacterController _controller;
        private MobileInput _input;
        private CharacterBase _character;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<MobileInput>();
            _character = GetComponent<CharacterBase>();

            if (cameraTransform != null)
            {
                return;
            }

            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                return;
            }

            cameraTransform = mainCamera.transform;
        }

        private void Update()
        {
            if (!_input.IsMoving)
            {
                return;
            }

            MoveCharacter();
        }

        private void MoveCharacter()
        {
            var moveSpeed = _character.GetStat(StatType.MoveSpeed);
            var moveDirection = GetMoveDirection();

            var velocity = moveDirection * moveSpeed;
            _controller.Move(velocity * Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                RotateCharacter(moveDirection);
            }
        }

        private Vector3 GetMoveDirection()
        {
            var inputDirection = _input.MoveInput;

            if (cameraTransform == null)
            {
                return new Vector3(inputDirection.x, 0f, inputDirection.y);
            }

            var cameraForward = cameraTransform.forward;
            var cameraRight = cameraTransform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            var moveDirection = (cameraForward * inputDirection.y) + (cameraRight * inputDirection.x);
            return moveDirection;
        }

        private void RotateCharacter(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}