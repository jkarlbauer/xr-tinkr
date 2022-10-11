using UnityEngine;
using UnityEngine.InputSystem;

namespace Xrtinkr.Input
{
    enum MoveDirection
    {
        Forward = 0,
        Backward = 1 ,
        Left = 2,
        Right = 3
    }

    public class DesktopPlayerController : MonoBehaviour
    {
        [SerializeField]
        InputActionReference forward;

        [SerializeField]
        InputActionReference backward;

        [SerializeField]
        InputActionReference left;

        [SerializeField]
        InputActionReference right;

        [SerializeField]
        InputActionReference look;

        [SerializeField]
        InputActionReference sprint;

        [SerializeField]
        private float _walkSpeed;

        [SerializeField]
        private float _lookSpeed;

        private float sprintFactor = 3;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            EnableActions();
            RegisterActionCallbacks();

        }

        private void EnableActions()
        {
            forward.action.Enable();
            backward.action.Enable();
            left.action.Enable();
            right.action.Enable();
            look.action.Enable();
            sprint.action.Enable();
        }

        private void RegisterActionCallbacks()
        {
            look.action.performed += OnLook;
            sprint.action.started += OnStartSprint;
            sprint.action.canceled += OnEndSprint;
        }

        private void Update()
        {
            if (forward.action.IsPressed())
            {
                Move(MoveDirection.Forward);
            }

            if (backward.action.IsPressed())
            {
                Move(MoveDirection.Backward);
            }

            if (right.action.IsPressed())
            {
                Move(MoveDirection.Right);
            }

            if (left.action.IsPressed())
            {
                Move(MoveDirection.Left);
            }        
        }

        private void OnStartSprint(InputAction.CallbackContext obj) => _walkSpeed *= sprintFactor;

        private void OnEndSprint(InputAction.CallbackContext obj) => _walkSpeed /= sprintFactor;

        private void OnLook(InputAction.CallbackContext obj)
        {
            Look();
        }

        private void Move(MoveDirection direction)
        {
            Vector3 directionVector = new Vector3();

            switch (direction)
            {
                case MoveDirection.Forward:
                    directionVector = transform.forward;
                    break;
                case MoveDirection.Backward:
                    directionVector = -transform.forward;
                    break;
                case MoveDirection.Right:
                    directionVector = transform.right;
                    break;
                case MoveDirection.Left:
                    directionVector = -transform.right;
                    break;
                default:
                    break;
            }
            
            transform.position += directionVector * _walkSpeed * Time.deltaTime;
        }

        private void Look()
        {
            Vector3 lookDelta = look.action.ReadValue<Vector2>();
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y += lookDelta.x * _lookSpeed;
            rotation.x -= lookDelta.y * _lookSpeed;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}

