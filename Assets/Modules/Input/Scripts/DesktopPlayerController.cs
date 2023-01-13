using UnityEngine;
using UnityEngine.InputSystem;

namespace Xrtinkr.Input
{
    enum MoveDirection
    {
        Forward = 0,
        Backward = 1 ,
        Left = 2,
        Right = 3,
        Up = 4,
        Down = 5,
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
        InputActionReference up;

        [SerializeField]
        InputActionReference down;

        [SerializeField]
        InputActionReference look;

        [SerializeField]
        InputActionReference sprint;

        [SerializeField]
        private float _walkSpeed;

        [SerializeField]
        private float _lookSpeed;

        private float _sprintSpeed = 3;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            EnableActions();
            RegisterActionCallbacks();

        }

        private void EnableActions()
        {
            InputActionReference[] actionReferences = new InputActionReference[] { 
                forward,
                backward,
                left,
                right,
                up,
                down,
                look,
                sprint
            };

            foreach(InputActionReference actionReference in actionReferences)
            {
                actionReference.action.Enable();
            }
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

            if (up.action.IsPressed())
            {
                Move(MoveDirection.Up);
            }

            if (down.action.IsPressed())
            {
                Move(MoveDirection.Down);
            }

            if (left.action.IsPressed())
            {
                Move(MoveDirection.Left);
            }        
        }

        private void OnStartSprint(InputAction.CallbackContext obj) => _walkSpeed *= _sprintSpeed;

        private void OnEndSprint(InputAction.CallbackContext obj) => _walkSpeed /= _sprintSpeed;

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
                case MoveDirection.Up:
                    directionVector = transform.up;
                    break;
                case MoveDirection.Down:
                    directionVector = -transform.up;
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

