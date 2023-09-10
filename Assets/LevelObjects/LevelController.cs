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

        [SerializeField]
        private AudioClip _music;
        [SerializeField]
        private AudioClip _winSound;
        [SerializeField]
        private AudioClip _loseSound;

        private Coroutine _spawnMobsCoroutine;

        private List<Animal> _spawnedMobs;
        private RoundConfiguration _currentRound;
        private int _currentIndexRound;

        public int CurrentLifes { get; private set; }
        public int MaxLifes => _levelConfiguration.Lifes;
        public int Money { get; private set; }
        public int CurrentRound => _currentIndexRound + 1;
        public int MaxRound => _levelConfiguration.Rounds.Count;

        public bool WithdrawMoney(int money)
        {
            if (Money - money < 0)
                return false;

            Money -= money;
            return true;
        }

        private void Start()
        {
            AudioController.Instance.SetMusic(_music);
            Money = _levelConfiguration.StartCountMoney;
            _spawnedMobs = new();
            _currentIndexRound = 0;
            CurrentLifes = MaxLifes;

            var towers = FindObjectsOfType<Tower>();
            for (int i = 0; i < towers.Length; i++)
                Destroy(towers[i].gameObject);

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
            mob.OnContactFinishZone += OnFinishMob;
            mob.transform.position = _spawnerMob.position;
            mob.SetMovementPoints(_movementPointsTargets.ToArray());
            _spawnedMobs.Add(mob);
        }

        private void OnFinishMob(Animal mob)
        {
            if (!mob.IsFedUp)
            {
                CurrentLifes -= mob.StealedLifes;
                if (CurrentLifes <= 0)
                {
                    CurrentLifes = 0;
                    GameOver();
                }
                return;
            }

            Money += mob.GivedMoney;
            _spawnedMobs.Remove(mob);
            Destroy(mob.gameObject);
            NextRount();
        }

        private void NextRount()
        {
            if (_spawnedMobs.Count > 0)
                return;

            _currentIndexRound++;
            if(_currentIndexRound >= MaxRound)
            {
                WinGame();
                return;
            }

            StartRound();
        }

        private void GameOver()
        {
            AudioController.Instance.PlaySound(_loseSound);

            _spawnedMobs.RemoveAll(mob => mob == null);
            for (int i = 0; i < _spawnedMobs.Count; i++)
                Destroy(_spawnedMobs[i].gameObject);

            ModelWindow.Instance.Show("Вы проиграли", "Начать занаво", () => { ModelWindow.Instance.Close(); Start(); });
        }

        private void WinGame()
        {
            AudioController.Instance.PlaySound(_winSound);
            ModelWindow.Instance.Show("Вы выиграли!", "Начать занаво", () => { ModelWindow.Instance.Close(); Start(); });
        }

        private IEnumerator SpawnMobs()
        {
            var totalSpawn = _currentRound.MobContainers.Sum(container => container.Count);
            var currentContainerIndex = 0;
            MobContainer currentContainer = _currentRound.MobContainers[currentContainerIndex];
            for (int countSpawns = 0; countSpawns < totalSpawn; countSpawns++)
            {
                SpawMob(currentContainer.AnimalPrefab);
                if (countSpawns == currentContainer.Count - 1 && currentContainerIndex + 1 < _currentRound.MobContainers.Count)
                {
                    currentContainerIndex++;
                    totalSpawn -= currentContainer.Count;
                    countSpawns = -1;
                    currentContainer = _currentRound.MobContainers[currentContainerIndex];
                }
                yield return new WaitForSeconds(currentContainer.DelaySpawnMob);
            }
        }
    }
}