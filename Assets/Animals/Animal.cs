using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using System;

namespace TowerDefense
{

    [System.Serializable]
    public struct FoodMultiplier
    {
        public FoodTypes FoodType;
        [Tooltip("This number is multiplied by 100%")]
        public float Multipler;
    }

    public abstract class Animal : TargetPoint
    {
        private List<Projectiles> _projectiles = new();
        private List<EffectSystem> _effects = new();

        [SerializeField]
        protected int _maxFood;
        [SerializeField]
        protected int _currentFood;
        [SerializeField]
        protected int _hasMoney;
        [SerializeField]
        protected int _stealLife;
        [SerializeField]
        protected List<FoodMultiplier> _foodsMultiplier;

        [SerializeField]
        protected MovementSystem _movementSystem;

        public abstract AnimalType Type { get; }
        public FoodTypes FoodEat 
        {
            get
            {
                var maxMultiplier = _foodsMultiplier.Max(multiplier => multiplier.Multipler);
                return _foodsMultiplier.Find(multiplier => multiplier.Multipler == maxMultiplier).FoodType;
            }
        }
        public int NeedFood => _maxFood;
        public bool IsFedUp => _currentFood >= _maxFood;
        public bool FullFed => _currentFood + _projectiles.Sum(x => CalculateTakeFood(x as FoodProjectiles)) >= _maxFood;
        public int GivedMoney => _hasMoney;
        public int StealedLifes => _stealLife;
        public MovementSystem MovementSystem => _movementSystem;

        public UnityAction<Animal> OnContactFinishZone;

        public bool HaveEffects<T>(out T effect) where T : EffectSystem
        {
            effect = null;
            if (_effects.Count == 0)
                return false;

            effect = _effects.Find(effect => effect.GetType() == typeof(T)) as T;
            return effect != null;
        }

        public void AddEffect(EffectSystem effect) => _effects.Add(effect);
        public void RemoveEffect(EffectSystem effect) => _effects.Remove(effect);

        public void SetMovementPoints(params TargetPoint[] targets)
        {
            _movementSystem.SetTargets(targets);
            _movementSystem.IsActive = true;
        }

        public void RememberProjectiles(Projectiles projectiles)
        {
            _projectiles.RemoveAll(projectiles => projectiles == null);
            _projectiles.Add(projectiles);
        }

        public void TakeFood(FoodProjectiles food)
        {
            _projectiles.Remove(food);
            _projectiles.RemoveAll(projectiles => projectiles == null);
            _currentFood += CalculateTakeFood(food);
            CheckFullFood();
        }
        public void TakeFood(FoodTypes type, int count)
        {
            _currentFood += CalculateTakeFood(type, count);
            CheckFullFood();
        }

        public int CalculateTakeFood(FoodTypes type, int count)
        {
            var multipler = _foodsMultiplier.Find(x => x.FoodType == type).Multipler;
            return Mathf.CeilToInt(count * multipler);
        }

        public int CalculateTakeFood(FoodProjectiles food) => CalculateTakeFood(food.OwnerTower.FoodType, food.OwnerTower.Damage);
    
        private void CheckFullFood()
        {
            if (!IsFedUp)
                return;

            _movementSystem.IsTurbo = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out FinishZone finish))
            {
                OnContactFinishZone.Invoke(this);
            }
        }
    }
}