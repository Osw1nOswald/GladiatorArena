using System.Collections;
using UnityEngine;
using UnityTutorial.Manager;

namespace UnityTutorial.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Настройки движения")]
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private float WalkSpeed = 2f;
        [SerializeField] private float RunSpeed = 6f;

        [Header("Настройки камеры")]
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField] private float CameraFollowSpeed = 20f; // сглаживание

        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;
        private int _attackHash;

        private Vector2 _currentVelocity;
        private float _xRotation;

        private Vector2 _smoothedLook;     // сглаживание мыши
        private Vector2 _currentLookVel;   // вспомогательная переменная для SmoothDamp

        private bool _isAttacking = false;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _playerRigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _attackHash = Animator.StringToHash("Attack");

            Debug.Log("PlayerController инициализирован без рывков");
        }

        private void Update()
        {
            HandleAttack();
        }

        private void FixedUpdate()
        {
            Move();
            RotatePlayer();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run ? RunSpeed : WalkSpeed;
            if (_inputManager.Move == Vector2.zero)
                targetSpeed = 0f;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            Vector3 targetVelocity = transform.TransformDirection(new Vector3(_currentVelocity.x, _playerRigidbody.linearVelocity.y, _currentVelocity.y));
            _playerRigidbody.linearVelocity = targetVelocity;

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void RotatePlayer()
        {
            // сглаживаем мышь, чтобы не было скачков
            _smoothedLook = Vector2.SmoothDamp(_smoothedLook, _inputManager.Look, ref _currentLookVel, 0.01f);

            transform.Rotate(Vector3.up, _smoothedLook.x * MouseSensitivity * Time.fixedDeltaTime);
        }

        private void CamMovements()
        {
            // Камера плавно следует за корнем
            Camera.position = Vector3.Lerp(Camera.position, CameraRoot.position, CameraFollowSpeed * Time.deltaTime);

            _xRotation -= _smoothedLook.y * MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        }

        private void HandleAttack()
        {
            if (!_hasAnimator) return;

            if (_inputManager.AttackPressed && !_isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        private IEnumerator AttackRoutine()
        {
            _isAttacking = true;
            _animator.SetTrigger(_attackHash);

            yield return new WaitForSeconds(GetAttackAnimationLength());
            _isAttacking = false;
        }

        private float GetAttackAnimationLength()
        {
            if (_animator == null) return 1f;

            var clips = _animator.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
            {
                if (clip.name.ToLower().Contains("attack"))
                    return clip.length;
            }
            return 1f;
        }
    }
}