using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace TowerDefense
{
    public class FoodTower : AttackTower<FoodProjectiles>, IUpgradebleFoodTower
    {
        [SerializeField]
        protected FoodTypes _foodType;
        [SerializeField]
        protected FoodTowerUpgradeConfiguration _upgradeConfiguration;

        protected int _currentLevelUpgrade;

        public override TowerTypes Type => TowerTypes.Food;
        public FoodTypes FoodType => _foodType;
        public int LevelUpgrade => _currentLevelUpgrade;
        public int TotalUpgrades => _upgradeConfiguration.TotalUpgrades;
        public event Action<Tower> OnUpgrade;
        public UpgradeLevel CurrentUpgradeLevel => UpgradeConfiguration.TakeUpgrade(LevelUpgrade);
        public FoodTowerUpgradeConfiguration UpgradeConfiguration => _upgradeConfiguration;

        protected override void Start()
        {
            base.Start();
            SetData(UpgradeConfiguration.TakeUpgrade(LevelUpgrade) as FoodTowerUpgrade);
        }

        private void SetData(FoodTowerUpgrade data)
        {
            _damage = data.Damage;
            _speedAttack = data.AttackSpeed;
            _projectilesPerShot = data.ProjectilesPerShot;
            _detectArea.Radius = data.AttackRange;
        }

        public void EncencingDamage(int count) => _damage = count;

        public void LevelUp()
        {
            _currentLevelUpgrade++;
            SetData(UpgradeConfiguration.TakeUpgrade(LevelUpgrade) as FoodTowerUpgrade);
            OnUpgrade?.Invoke(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            AudioController.Instance.PlayClick();
            TowerUpgradeController.Instance.ShowUpgradeWindow(this);
        }

        protected override FoodProjectiles CreateProjectiles()
        {
            var projectiles = base.CreateProjectiles();
            projectiles.Configurate(this, _target);
            return projectiles;
        }

        public UpgradeLevel TakeUpgrade(int index) => _upgradeConfiguration.TakeUpgrade(index);
    }
}