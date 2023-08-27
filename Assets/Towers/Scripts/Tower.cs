using UnityEngine;

namespace TowerDefense
{
    public abstract class Tower : MonoBehaviour
    {
        protected DetectArea _detectArea;

        public abstract TowerTypes Type { get; }

        protected void Awake()
        {
            _detectArea = GetComponentInChildren<DetectArea>(true);
        }
    }
}