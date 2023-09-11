using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense
{
    public class DetectArea : MonoBehaviour
    {
        [SerializeField]
        private CircleCollider2D _collider;

        private List<Tower> _towers = new();
        private List<Animal> _animals = new();

        public event Action<Animal> OnEnterFirstAnimal; 

        public float Radius { get => _collider.radius; set => _collider.radius = value; }

        public bool GetFirstAnimal(out Animal animal)
        {
            animal = null;
            if (_animals.Count < 1 || _animals.All(animal => animal.IsFedUp))
                return false;

            animal = _animals.FirstOrDefault(animal => !animal.IsFedUp && !animal.FullFed);
            return animal != null;
        }

        public bool CheckAnimalInArea(Animal animal) => _animals.Contains(animal);

        public IReadOnlyList<Animal> GetAnimals() => new List<Animal>(_animals);

        public bool GetAllTowers(out IReadOnlyList<Tower> towers)
        {
            towers = null;
            if(_towers.Count < 1) 
                return false;

            towers = new List<Tower>(_towers);
            return true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Tower tower))
            {
                _towers.Add(tower);
            }
            else if(collision.TryGetComponent(out Animal animal))
            {
                _animals.Add(animal);

                if (_animals.Count == 1 && !animal.IsFedUp)
                    OnEnterFirstAnimal?.Invoke(animal);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Tower tower))
            {
                _towers.Remove(tower);
            }
            else if (collision.TryGetComponent(out Animal animal))
            {
                _animals.Remove(animal);
            }
        }
    }
}