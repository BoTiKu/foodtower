using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class StoreItemGUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private TMP_Text _cost;

        [SerializeField]
        private float _toolTipOffsetY = 2.5f;
        [SerializeField]
        private float _toolTipOffsetX = 5;

        private Camera _camera;
        private ItemConfiguration _data;

        public void OnPointerDown(PointerEventData eventData)
        {
            AudioController.Instance.PlayClick();
            if (!StoreController.Instance.CanBuyItem(_data))
            {
                ModelWindow.Instance.Show($"У вас недостаточно денег.\nНеобходимо: {_data.Cost}", "Окей", ModelWindow.Instance.Close);
                return;
            }

            if(_data.Type == StoreItemTypes.Towrer)
                StoreController.Instance.CreateDummyTower(_data as TowerItemConfiguration);

            if (_data.Type == StoreItemTypes.Effects)
                (_data as EffectsItemConfigurate)?.Execute();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 position = transform.position;
            position.y += _toolTipOffsetY;
            position.x += _toolTipOffsetX;
            ModelWindow.Instance.ShowToolTip(_data.Description, position);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ModelWindow.Instance.CloseToolTip();
        }

        public void SetData(ItemConfiguration data)
        {
            _data = data;
            _icon.sprite = data.Icon;
            _title.text = data.Name;
            _cost.text = $"Cost: {data.Cost}";
        }

        private void Start()
        {
            _camera = Camera.main;
        }
    }
}