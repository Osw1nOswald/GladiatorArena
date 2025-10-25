using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTutorial.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }
        public bool AttackTriggered { get; private set; }

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _attackAction;

        private void Awake()
        {
            HideCursor();

            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction = _currentMap.FindAction("Run");
            _attackAction = _currentMap.FindAction("Attack");

            // Отладка
            if (_attackAction == null)
            {
                Debug.LogError("❌ Attack action не найдено!");
            }
            else
            {
                Debug.Log("✅ Attack action найдено и подключено!");
            }

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun;
            _attackAction.performed += onAttack;

            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;
        }

        private void Update()
        {
            // Сбрасываем флаг атаки каждый кадр
            AttackTriggered = false;
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void onAttack(InputAction.CallbackContext context)
        {
            AttackTriggered = true;
            Debug.Log("🔥 ATTACK! Клавиша E нажата!");
        }
    }
}
