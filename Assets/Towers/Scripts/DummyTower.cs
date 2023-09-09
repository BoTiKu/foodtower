using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

namespace TowerDefense
{
    public class DummyTower : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRender;

        [SerializeField]
        private Color _normalColor, _cantBuildColor;

        private Sprite Icon
        {
            get => _spriteRender.sprite;
            set => _spriteRender.sprite = value;
        }
        private Color IconColor
        {
            get => _spriteRender.color;
            set => _spriteRender.color = value;
        }

        private Vector3 _offset;
        private Camera _camera;
        private TowerItemConfiguration _data;
        private List<Collider2D> _contacts = new();

        public Vector2 CurrentPosition => new(transform.position.x, transform.position.y);
        public bool CanBuild => _contacts.Count == 0;

        private void Awake()
        {
            _camera = Camera.main;
            _offset = gameObject.transform.position -
                _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        }

        private void Update()
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            transform.position = _camera.ScreenToWorldPoint(newPosition);

            if (Input.GetMouseButtonUp(0))
                MouseUp();
        }

        public void MouseUp()
        {
            if(CanBuild)
            {
                Instantiate(_data.TowerPrefab, CurrentPosition, Quaternion.identity);
                LevelController.Instance.WithdrawMoney(_data.Cost);
            }
            Destroy(gameObject);
        }

        public void SetData(TowerItemConfiguration data)
        {
            _data = data;
            Icon = data.Icon;
            gameObject.name = $"DummyTower_{data.Name}";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger)
            {
                _contacts.Add(collision);
                IconColor = _cantBuildColor;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _contacts.Remove(collision);
            if (CanBuild)
                IconColor = _normalColor;
        }
    }
}