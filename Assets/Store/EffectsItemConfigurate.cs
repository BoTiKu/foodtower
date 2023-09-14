using UnityEngine;

namespace TowerDefense
{
    public interface IExecutable
    {
        void Execute();
    }

    public abstract class EffectsItemConfigurate : ItemConfiguration, IExecutable
    {
        public float ActiveTime;
        public override StoreItemTypes Type => StoreItemTypes.Effects;
        public virtual void Execute()
        {
            LevelController.Instance.WithdrawMoney(Cost);
            AudioController.Instance.PlaySuccess();
        }
    }

}