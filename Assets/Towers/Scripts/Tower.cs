using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public abstract class Tower : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected Sprite _towerSprite;

        protected DetectArea _detectArea;
        protected SpriteRenderer _spriteRenderer;

        public abstract TowerTypes Type { get; }

        public abstract void OnPointerClick(PointerEventData eventData);

        protected void Awake()
        {
            _detectArea = GetComponentInChildren<DetectArea>(true);
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _spriteRenderer.sprite = _towerSprite;
        }
    }
}