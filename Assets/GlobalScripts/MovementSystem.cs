using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _turboSpeed;

        private Rigidbody2D _rigidbody2D;

        private Queue<TargetPoint> _targetsPoints;

        private TargetPoint NextTargetPoint => _targetsPoints.Dequeue();
        private TargetPoint _currentTarget;

        public bool IsActive { get; set; }
        public bool IsTurbo { get; set; }

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!IsActive)
                return;

            MoveToTarget();
        }

        public void SetTargets(params TargetPoint[] targets)
        {
            _targetsPoints = new Queue<TargetPoint>(targets);
            _currentTarget = NextTargetPoint;
        }

        private void MoveToTarget()
        {
            if(_currentTarget == null)
            {
                _rigidbody2D.velocity = Vector3.zero;
                return;
            }

            Vector2 targetPosition = _currentTarget.transform.position;
            Vector2 desired_velocity =  (IsTurbo ? _turboSpeed : _speed) * Time.fixedDeltaTime * (targetPosition - (Vector2)transform.position).normalized;
            Vector2 steering = desired_velocity - _rigidbody2D.velocity;

            _rigidbody2D.velocity += steering;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TargetPoint point) && point == _currentTarget)
            {
                _currentTarget = _targetsPoints.Count > 0 ? NextTargetPoint : null;
            }
        }
    }
}