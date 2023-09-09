using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "Tower Item Configuration", menuName = "Game/Tower Item Configuration", order = 51)]
    public class TowerItemConfiguration : ItemConfiguration
    {
        public override StoreItemTypes Type => StoreItemTypes.Towrer;
        public Tower TowerPrefab;
    }
}