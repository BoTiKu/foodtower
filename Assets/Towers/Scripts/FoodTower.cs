using UnityEngine;
using UnityEngine.EventSystems;

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
        public UpgradeLevel CurrentUpgradeLevel => UpgradeConfiguration.TakeUpgrae(LevelUpgrade);
        public FoodTowerUpgradeConfiguration UpgradeConfiguration { get => _upgradeConfiguration; }

        protected override void Start()
        {
            base.Start();
            SetData(UpgradeConfiguration.TakeUpgrae(LevelUpgrade));
        }

        private void SetData(UpgradeLevel data)
        {
            _damage = data.Damage;
            _speedAttack = data.AttackSpeed;
            _projectilesPerShot = data.ProjectilesPerShot;
            _detectArea.Radius = data.AttackRange;
        }

        public void LevelUp()
        {
            _currentLevelUpgrade++;
            SetData(UpgradeConfiguration.TakeUpgrae(LevelUpgrade));
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
    }
}