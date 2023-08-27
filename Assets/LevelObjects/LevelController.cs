using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TowerDefense
{
    public class LevelController : MonoSingleton<LevelController>
    {
        [SerializeField]
        private List<TargetPoint> _movementPointsTargets;
        [SerializeField]
        private LevelConfiguration _levelConfiguration;
        [SerializeField]
        private Transform _spawnerMob;

        private Coroutine _spawnMobsCoroutine;

        private RoundConfiguration _currentRound;
        private int _currentIndexRound;

        private void Start()
        {
            _currentIndexRound = 0;
            StartRound();
        }

        private void StartRound()
        {
            _currentRound = _levelConfiguration.Rounds[_currentIndexRound];
            _spawnMobsCoroutine = StartCoroutine(SpawnMobs());
        }

        private void SpawMob(Animal prefab)
        {
            Animal mob = Instantiate(prefab);
            mob.transform.position = _spawnerMob.position;
            mob.SetMovementPoints(_movementPointsTargets.ToArray());
        }

        private IEnumerator SpawnMobs()
        {
            var totalSpawn = _currentRound.MobContainers.Sum(container => container.Count);
            var currentContainerIndex = 0;
            for (int countSpawns = 0; countSpawns < totalSpawn; countSpawns++)
            {
                var currentContainer = _currentRound.MobContainers[currentContainerIndex];
                SpawMob(currentContainer.AnimalPrefab);
                yield return new WaitForSeconds(currentContainer.DelaySpawnMob);

                if(countSpawns == currentContainer.Count)
                {
                    currentContainerIndex++;
                    totalSpawn -= currentContainer.Count;
                    countSpawns = 0;
                }
            }
        }
    }
}