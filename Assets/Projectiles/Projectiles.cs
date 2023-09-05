using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectiles : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        protected Animal _target;

        [SerializeField]
        protected MovementSystem _movementSystem;
        
        public Sprite Icon
        {
            get => _spriteRenderer.sprite;
            set => _spriteRenderer.sprite = value;
        }

        private void Start () 
        {
            _movementSystem.IsActive = true;
        }

        private void Update()
        {
            if (_target != null)
                _movementSystem.SetTargets(_target);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Animal target) && target == _target)
            {
                ReachTarget();
                Destroy(gameObject);
            }
        }

        protected abstract void ReachTarget();
    }
}