using System.Collections;
using System.Collections.Generic;
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

        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _hasAnimator;

        private int _xVelHash;
        private int _yVelHash;
        private int _attackHash;

        private Vector2 _currentVelocity;
        private float _xRotation;

        private bool _isAttacking = false;

        private void Start()
        {
            _hasAnimator = TryGetComponent(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _attackHash = Animator.StringToHash("Attack");

            Debug.Log("PlayerController инициализирован");
        }

        private void Update()
        {
            HandleAttack();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return; // во время атаки двигаемся

            float targetSpeed = _inputManager.Run ? RunSpeed : WalkSpeed;
            if (_inputManager.Move == Vector2.zero)
                targetSpeed = 0.1f;

            _currentVelocity.x = Mathf.Lerp(
                _currentVelocity.x,
                _inputManager.Move.x * targetSpeed,
                AnimBlendSpeed * Time.fixedDeltaTime);

            _currentVelocity.y = Mathf.Lerp(
                _currentVelocity.y,
                _inputManager.Move.y * targetSpeed,
                AnimBlendSpeed * Time.fixedDeltaTime);

            var xVelDifference = _currentVelocity.x - _playerRigidbody.linearVelocity.x;
            var zVelDifference = _currentVelocity.y - _playerRigidbody.linearVelocity.z;

            _playerRigidbody.AddForce(
                transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)),
                ForceMode.VelocityChange);

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            float Mouse_X = _inputManager.Look.x;
            float Mouse_Y = _inputManager.Look.y;

            Camera.position = CameraRoot.position;

            _xRotation -= Mouse_Y * MouseSensitivity * Time.deltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, Mouse_X * MouseSensitivity * Time.deltaTime);
        }

        private void HandleAttack()
        {
            if (!_hasAnimator) return;

            // Если атака нажата и сейчас не идёт другая атака
            if (_inputManager.AttackPressed && !_isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }

        private IEnumerator AttackRoutine()
        {
            _isAttacking = true;
            _animator.SetTrigger(_attackHash);

            // ждём пока анимация проиграется
            yield return new WaitForSeconds(GetAttackAnimationLength());
            _isAttacking = false;
        }

        private float GetAttackAnimationLength()
        {
            // попытаемся получить длину текущей анимации атаки из Animator
            if (_animator == null) return 1f;
            var clips = _animator.runtimeAnimatorController.animationClips;
            foreach (var clip in clips)
            {
                if (clip.name.ToLower().Contains("attack"))
                    return clip.length;
            }
            return 1f; // если не нашли — значение по умолчанию
        }
    }
}
