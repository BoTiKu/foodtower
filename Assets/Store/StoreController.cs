using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace TowerDefense
{
    public class StoreController : MonoSingleton<StoreController>
    {
        [SerializeField]
        private GameObject _panel;
        [SerializeField]
        private TMP_Text _buttonTitle;
        [SerializeField]
        private StoreItemGUI _itemPrefab;
        [SerializeField]
        private DummyTower _dummyTower;
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private List<ItemConfiguration> _itemsContainer = new();

        private const string SHOW_STORE_TEXT = "Show Store";
        private const string CLOSE_STORE_TEXT = "Close Store";

        private bool IsShowing => _panel.activeSelf;

        private List<StoreItemGUI> _itemsGUI = new();

        public void OnClickChangeActiveStore()
        {
            _panel.SetActive(!IsShowing);
            _buttonTitle.text = IsShowing ? CLOSE_STORE_TEXT : SHOW_STORE_TEXT;
        }

        private void Start()
        {
            GenerateItems();
        }

        public bool CanBuyItem(ItemConfiguration data) => LevelController.Instance.Money >= data.Cost;

        public DummyTower CreateDummyTower(TowerItemConfiguration data)
        {
            DummyTower dummy = Instantiate(_dummyTower);
            dummy.SetData(data);
            dummy.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            return dummy;
        }

        private void GenerateItems()
        {
            _itemsContainer.ForEach(configuration => 
            {
                var itemGUI = Instantiate(_itemPrefab, _content);
                itemGUI.SetData(configuration);
                _itemsGUI.Add(itemGUI);
            });
        }
    }
}