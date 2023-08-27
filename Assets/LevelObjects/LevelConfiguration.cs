using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    [System.Serializable]
    public struct MobContainer
    {
        public Animal AnimalPrefab;
        public float DelaySpawnMob;
        public int Count;
    }
    [System.Serializable]
    public struct RoundConfiguration
    {
        public List<MobContainer> MobContainers;
    }

    [CreateAssetMenu(fileName = "Level Configuration", menuName = "Game/Level Configuration", order = 51)]
    public class LevelConfiguration : ScriptableObject
    {
        public List<RoundConfiguration> Rounds;
    }
}