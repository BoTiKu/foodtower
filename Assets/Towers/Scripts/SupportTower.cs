using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace TowerDefense
{
    public class SupportTower : Tower, IUpgradebleSupportTower
    {
        [SerializeField]
        protected float _enhancingEffect;

        [SerializeField]
        protected SupportTowerUpgradeConfiguration _upgradeConfiguration;

        protected int _currentLevelUpgrade;

        public override TowerTypes Type => TowerTypes.Boost;

        public SupportTowerUpgradeConfiguration UpgradeConfiguration => _upgradeConfiguration;

        public event Action<Tower> OnUpgrade;

        public UpgradeLevel CurrentUpgradeLevel => UpgradeConfiguration.TakeUpgrade(LevelUpgrade);

        public int LevelUpgrade => _currentLevelUpgrade;

        public int TotalUpgrades => _upgradeConfiguration.TotalUpgrades;

        protected void Start()
        {
            _detectArea.OnAttachedNewTower += AttachedNewTowerHandler;
            SetData(UpgradeConfiguration.TakeUpgrade(LevelUpgrade) as SupportTowerUpgrade);
        }

        protected void AttachedNewTowerHandler(Tower target)
        {
            if (target is not FoodTower foodTower)
                return;

            foodTower.OnUpgrade += EnchancingTower;
            EnchancingTower(foodTower);
        }

        protected void EnchancingTower(Tower target)
        {
            if (target.Type != TowerTypes.Food)
                return;
            var foodTower = target as FoodTower;
            var upgradeLvl = foodTower.CurrentUpgradeLevel as FoodTowerUpgrade;
            var calculateDamage = Mathf.RoundToInt(upgradeLvl.Damage * _enhancingEffect);
            var effect = target.FindEffect(TowerEffectTypes.EnhancingDamage);

            if(effect == null)
            {
                effect = new EffectEntity(calculateDamage, TowerEffectTypes.EnhancingDamage);
                target.AddEffect(effect);
                foodTower.EncencingDamage(upgradeLvl.Damage + calculateDamage);
            }

            if (effect.Value <= calculateDamage)
            {
                effect.Value = calculateDamage;
                foodTower.EncencingDamage(upgradeLvl.Damage + calculateDamage);
            }
        }

        public void LevelUp()
        {
            _currentLevelUpgrade++;
            SetData(UpgradeConfiguration.TakeUpgrade(LevelUpgrade) as SupportTowerUpgrade);
            OnUpgrade?.Invoke(this);

            if(_detectArea.GetAllTowers(out IReadOnlyList<Tower> towers))
            {
                foreach (var tower in towers)
                {
                    EnchancingTower(tower);
                }
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            AudioController.Instance.PlayClick();
            TowerUpgradeController.Instance.ShowUpgradeWindow(this);
        }

        public UpgradeLevel TakeUpgrade(int index) => _upgradeConfiguration.TakeUpgrade(index);

        private void SetData(SupportTowerUpgrade data)
        {
            _detectArea.Radius = data.Range;
            _enhancingEffect = data.EnhancingEffect;
        }
    }
}