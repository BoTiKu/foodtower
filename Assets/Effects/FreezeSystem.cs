using UnityEngine;

namespace TowerDefense
{
    public class FreezeSystem : EffectSystem
    {
        private float _remainedSpeed;
        private FreezeEffectConfiguration _configurate;

        public override void SetUp(EffectsItemConfigurate data)
        {
            base.SetUp(data);
            _configurate = data as FreezeEffectConfiguration;
        }

        public override void CancelEffect()
        {
            if (_target.IsFedUp)
                return;

            _target.MovementSystem.Speed = _remainedSpeed;
            _target.RemoveEffect(this);
        }

        public override void Execute()
        {
            if (_target.IsFedUp)
                return;

            _remainedSpeed = _target.MovementSystem.Speed;
            var calculateSpeed = _target.MovementSystem.Speed - (_target.MovementSystem.Speed * _configurate.SlowingDown);
            _target.MovementSystem.Speed = calculateSpeed;
            _target.AddEffect(this);
        }
    }
}