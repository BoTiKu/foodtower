using System.Collections;
using UnityEngine;

namespace TowerDefense
{
    public abstract class AttackTower<T> : Tower where T : Projectiles
    {
        [SerializeField]
        protected AttckTypes _attackType;
        [SerializeField]
        protected T _projectilesPrefab;

        [Header("Параметры атаки")]
        [SerializeField]
        protected float _speedAttack;
        [SerializeField]
        protected int _damage;
        [SerializeField]
        protected int _projectilesPerShot = 1;

        protected bool CanShoot { get; set; } = true;
        protected Animal _target; 

        public AttckTypes AttckType => _attackType;
        public int Damage => _damage; 

        protected virtual void Start()
        {
            _detectArea.OnEnterFirstAnimal += Shoot;
        }

        protected void Shoot(Animal target)
        {
            _target = target;

            if(_target != null && (_target.FullFed || !_detectArea.CheckAnimalInArea(_target)) )
                _detectArea.GetFirstAnimal(out _target);

            if (!CanShoot || (_target == null && !_detectArea.GetFirstAnimal(out _target)) )
                return;

            for (int i = 0; i < _projectilesPerShot; i++)
            {
                var projectiles = CreateProjectiles();
                _target.RememberProjectiles(projectiles);
            }
            CanShoot = false;
            StartCoroutine(Reload());
        }

        private IEnumerator Reload()
        {
            var time = 0f;
            while (time < _speedAttack)
            {
                time += Time.deltaTime;
                yield return null;
            }
            CanShoot = true;
            Shoot(_target);
        }

        protected virtual T CreateProjectiles()
        {
            var projectiles = Instantiate(_projectilesPrefab);
            projectiles.transform.position = transform.position;
            return projectiles;
        }
    }
}

