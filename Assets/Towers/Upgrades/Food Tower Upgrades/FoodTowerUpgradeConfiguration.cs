using UnityEngine;
using System.Collections.Generic;
using System;

namespace TowerDefense
{
    [Serializable]
    public struct UpgradeLevel
    {
        public float AttackSpeed;
        public int Damage;
        public int ProjectilesPerShot;
        public int AttackRange;
        public int CostToUpp;
        public string Description;
        public Sprite Icon;
        public string Name;
    }

    [CreateAssetMenu(fileName = "Food Tower Configuration", menuName = "Game/Tower Configuration/Food Tower", order = 51)]
    public class FoodTowerUpgradeConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<UpgradeLevel> _upgrades;

        public FoodTypes FoodTarget;
        public int TotalUpgrades => _upgrades.Count;
        public UpgradeLevel TakeUpgrae(int index)
        {
            if (index < 0 || index > TotalUpgrades - 1)
                throw new ArgumentOutOfRangeException("index", "По такому индексу нет апгрейда");

            return _upgrades[index];
        }
    }
}