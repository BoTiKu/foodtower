using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

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
        private List<Projectiles> _projectiles = new List<Projectiles>();

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
        public bool IsFedUp => _currentFood >= _maxFood;
        public bool FullFed => _currentFood + _projectiles.Sum(x => CalculateTakeFood(x as FoodProjectiles)) >= _maxFood;
        public int GivedMoney => _hasMoney;
        public int StealedLifes => _stealLife;

        public UnityAction<Animal> OnContactFinishZone;

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

        public virtual int CalculateTakeFood(FoodProjectiles food)
        {
            var value = food.OwnerTower.Damage;
            var foodType = food.OwnerTower.FoodType;
            var multipler = _foodsMultiplier.FirstOrDefault(x => x.FoodType == foodType).Multipler;

            return Mathf.CeilToInt(value * multipler);
        }
    
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