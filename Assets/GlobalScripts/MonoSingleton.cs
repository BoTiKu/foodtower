using UnityEngine;

namespace TowerDefense
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning($"Remove dublicate: {typeof(T).Name} -> {gameObject.name}");
                Destroy(this);
            }

            _instance = this as T;
            Initialize();
        }

        private void OnDestroy()
        {
            _instance = null;
        }

        protected virtual void Initialize() { }
    }
}