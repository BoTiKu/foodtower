using UnityEngine;

namespace TowerDefense
{
    public enum StoreItemTypes
    {
        Towrer, Effects
    }

    public abstract class ItemConfiguration : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public int Cost;
        public abstract StoreItemTypes Type { get; }
    }
}