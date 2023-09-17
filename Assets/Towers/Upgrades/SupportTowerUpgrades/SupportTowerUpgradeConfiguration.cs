using UnityEngine;
using System;
using System.Collections.Generic;

namespace TowerDefense
{
    [Serializable]
    public class SupportTowerUpgrade : UpgradeLevel
    {
        public float EnhancingEffect;
        public int Range;
    }

    [CreateAssetMenu(fileName = "Support Tower Configuration", menuName = "Game/Tower Configuration/Support Tower", order = 51)]
    public class SupportTowerUpgradeConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<SupportTowerUpgrade> _upgrades;
        public int TotalUpgrades => _upgrades.Count;
        public UpgradeLevel TakeUpgrade(int index)
        {
            if (index < 0 || index > TotalUpgrades - 1)
                throw new ArgumentOutOfRangeException("index", "По такому индексу нет апгрейда");

            return _upgrades[index];
        }
    }
}