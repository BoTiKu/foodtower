using UnityEngine;

namespace TowerDefense
{
    public interface ICancelEffect
    {
        void CancelEffect();
    }

    public interface IReload
    {
        void Reload();
    }
    public abstract class EffectSystem : MonoBehaviour, IExecutable, ICancelEffect, IReload
    {
        protected EffectsItemConfigurate _data;
        protected Animal _target;
        protected float _lifeTime;

        public abstract void Execute();
        public abstract void CancelEffect();

        private void Start()
        {
            _target = GetComponent<Animal>();

            if (_target == null)
                Destroy(this);

            Execute();
        }

        public virtual void Reload()
        {
            _lifeTime = 0;
        }

        private void Update()
        {
            _lifeTime += Time.deltaTime;
            if(_lifeTime >= _data.ActiveTime)
            {
                CancelEffect();
                Destroy(this);
            }
        }

        public virtual void SetUp(EffectsItemConfigurate data)
        {
            _data = data;
        }

    }
}