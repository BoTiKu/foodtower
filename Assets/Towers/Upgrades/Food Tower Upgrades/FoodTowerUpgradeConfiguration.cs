using UnityEngine;
using System.Collections.Generic;
using System;

namespace TowerDefense
{

    [Serializable]
    public class FoodTowerUpgrade : UpgradeLevel
    {
        public float AttackSpeed;
        public int Damage;
        public int ProjectilesPerShot;
        public int AttackRange;
    }

    [CreateAssetMenu(fileName = "Food Tower Configuration", menuName = "Game/Tower Configuration/Food Tower", order = 51)]
    public class FoodTowerUpgradeConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<FoodTowerUpgrade> _upgrades;

        public FoodTypes FoodTarget;
        public int TotalUpgrades => _upgrades.Count;
        public UpgradeLevel TakeUpgrade(int index)
        {
            if (index < 0 || index > TotalUpgrades - 1)
                throw new ArgumentOutOfRangeException("index", "По такому индексу нет апгрейда");

            return _upgrades[index];
        }
    }
}