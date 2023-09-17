using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace TowerDefense
{
    public abstract class Tower : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected Sprite _towerSprite;

        protected DetectArea _detectArea;
        protected SpriteRenderer _spriteRenderer;
        protected List<EffectEntity> _effects;

        public abstract TowerTypes Type { get; }

        public abstract void OnPointerClick(PointerEventData eventData);

        protected void Awake()
        {
            _detectArea = GetComponentInChildren<DetectArea>(true);
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _spriteRenderer.sprite = _towerSprite;
            _effects = new();
        }

        public EffectEntity FindEffect(TowerEffectTypes type) => _effects.Find(effect => effect.Type == type);
        public void AddEffect(EffectEntity effect) => _effects.Add(effect);
        public bool RemoveEffect(EffectEntity effect) => _effects.Remove(effect);
    }
}