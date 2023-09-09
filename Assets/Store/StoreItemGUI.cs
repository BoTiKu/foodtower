using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class StoreItemGUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private TMP_Text _cost;

        private ItemConfiguration _data;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!StoreController.Instance.CanBuyItem(_data))
            {
                ModelWindow.Instance.Show($"У вас недостаточно денег.\nНеобходимо: {_data.Cost}", "Окей", ModelWindow.Instance.Close);
                return;
            }

            if(_data.Type == StoreItemTypes.Towrer)
                StoreController.Instance.CreateDummyTower(_data as TowerItemConfiguration);
        }

        public void SetData(ItemConfiguration data)
        {
            _data = data;
            _icon.sprite = data.Icon;
            _title.text = data.Name;
            _cost.text = $"Cost: {data.Cost}";
        }
    }
}