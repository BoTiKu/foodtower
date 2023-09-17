using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    public interface IUpgradebleTower
    {
        event Action<Tower> OnUpgrade;
        UpgradeLevel CurrentUpgradeLevel { get; }
        int LevelUpgrade { get; }
        int TotalUpgrades { get; }
        void LevelUp();
        UpgradeLevel TakeUpgrade(int index);
    }

    public interface IUpgradebleFoodTower : IUpgradebleTower
    {
        FoodTowerUpgradeConfiguration UpgradeConfiguration { get; }
    }

    public interface IUpgradebleSupportTower : IUpgradebleTower
    {
        SupportTowerUpgradeConfiguration UpgradeConfiguration { get; }
    }

    [Serializable]
    public class UpgradeLevel
    {
        public int CostToUpp;
        public string Description;
        public Sprite Icon;
        public string Name;
    }

    public class TowerUpgradeController : MonoSingleton<TowerUpgradeController>
    {
        [Serializable]
        public struct WindowStructure
        {
            public Image Icon;
            public TMP_Text Name;
            public TMP_Text Cost;
            public TMP_Text Description;
            public Button UpgradeButton;
        }

        [SerializeField]
        protected Canvas _window;
        [SerializeField]
        protected WindowStructure _windowStructure;

        protected IUpgradebleTower _target;

        public void ShowUpgradeWindow(IUpgradebleTower target)
        {
            _target = target;
            StoreController.Instance.CloseWindow();
            SetUpWindow();
            _window.enabled = true;
        }

        protected void SetUpWindow()
        {
            if(_target.LevelUpgrade + 1 >= _target.TotalUpgrades)
            {
                ShowMaxUpgrades();
                return;
            }

            var cost = _target.CurrentUpgradeLevel.CostToUpp;
            var nextUp = _target.TakeUpgrade(_target.LevelUpgrade + 1);
            _windowStructure.Icon.sprite = nextUp.Icon;
            _windowStructure.Description.text = nextUp.Description;
            _windowStructure.Name.text = nextUp.Name;
            _windowStructure.Cost.text = $"Price: {cost}";
            _windowStructure.UpgradeButton.interactable = true;
        }

        protected void ShowMaxUpgrades()
        {
            var currentUp = _target.CurrentUpgradeLevel;
            _windowStructure.UpgradeButton.interactable = false;
            _windowStructure.Cost.text = "";
            _windowStructure.Name.text = currentUp.Name;
            _windowStructure.Icon.sprite = currentUp.Icon;
            _windowStructure.Description.text = "Have max upgrades";
        }

        public void CloseWindow()
        {
            _target = null;
            _window.enabled = false;
        }

        public void OnClickUpgrade()
        {
            var needMoney = _target.CurrentUpgradeLevel.CostToUpp;
            if (LevelController.Instance.Money >= needMoney)
            {
                AudioController.Instance.PlaySuccess();
                LevelController.Instance.WithdrawMoney(needMoney);
                _target.LevelUp();
                SetUpWindow();
                return;
            }

            AudioController.Instance.PlayFailed();
            ModelWindow.Instance.Show($"Can't Upgrade\nYou need {needMoney} to upgrade", "Okey", ModelWindow.Instance.Close);
        }

    }
}