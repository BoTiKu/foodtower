using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    public interface IUpgradebleFoodTower
    {
        FoodTowerUpgradeConfiguration UpgradeConfiguration { get; }
        UpgradeLevel CurrentUpgradeLevel { get; }
        int LevelUpgrade { get; }
        void LevelUp();
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

        protected IUpgradebleFoodTower _target;

        public void ShowUpgradeWindow(IUpgradebleFoodTower target)
        {
            _target = target;
            StoreController.Instance.CloseWindow();
            SetUpWindow();
            _window.enabled = true;
        }

        protected void SetUpWindow()
        {
            if(_target.LevelUpgrade + 1 >= _target.UpgradeConfiguration.TotalUpgrades)
            {
                ShowMaxUpgrades();
                return;
            }

            var cost = _target.CurrentUpgradeLevel.CostToUpp;
            var nextUp = _target.UpgradeConfiguration.TakeUpgrae(_target.LevelUpgrade + 1);
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
            ModelWindow.Instance.Show("Can't Upgrade", $"You need {needMoney} to upgrade", ModelWindow.Instance.Close);
        }

    }
}