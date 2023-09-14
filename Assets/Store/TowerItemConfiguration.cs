using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu(fileName = "Tower Item Configuration", menuName = "Game/Store/Towers/Tower Item Configuration", order = 51)]
    public class TowerItemConfiguration : ItemConfiguration
    {
        public override StoreItemTypes Type => StoreItemTypes.Towrer;
        public Tower TowerPrefab;
    }
}