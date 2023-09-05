using UnityEngine;

namespace TowerDefense
{
    public abstract class Tower : MonoBehaviour
    {
        [SerializeField]
        protected Sprite _towerSprite;

        protected DetectArea _detectArea;
        protected SpriteRenderer _spriteRenderer;

        public abstract TowerTypes Type { get; }

        protected void Awake()
        {
            _detectArea = GetComponentInChildren<DetectArea>(true);
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _spriteRenderer.sprite = _towerSprite;
        }
    }
}