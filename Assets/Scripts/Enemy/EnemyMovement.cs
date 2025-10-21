using System;
using UnityEngine;

namespace UnityTutorial.EnemyControl
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform target; // цель, например, игрок
        private Rigidbody _rb;
        private Animator _animator;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private Vector2 _currentVelocity;
        private const float _walkSpeed = 2f;

        private void Start()
        {
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _rb = GetComponent<Rigidbody>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
        }

        private void FixedUpdate()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (!_hasAnimator || target == null) return;

            Vector3 direction = (target.position - transform.position);
            direction.y = 0; // оставляем только горизонтальную плоскость
            Vector3 moveDirection = direction.normalized;

            float targetSpeed = _walkSpeed;

            _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, 0, AnimBlendSpeed * Time.fixedDeltaTime); // нет бокового смещения
            _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

            var xVelDifference = _currentVelocity.x - _rb.linearVelocity.x;
            var zVelDifference = _currentVelocity.y - _rb.linearVelocity.z;

            Vector3 force = transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference));
            _rb.AddForce(force, ForceMode.VelocityChange);

            // Поворачиваемся в сторону игрока
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, AnimBlendSpeed * Time.fixedDeltaTime);
            }

            // Анимация вперед
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }
    }
}
