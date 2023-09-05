using UnityEngine;
using TMPro;

namespace TowerDefense
{
    public class LevelInfoDrawer : MonoSingleton<LevelInfoDrawer>
    {
        [SerializeField]
        private TMP_Text _roundsText;
        [SerializeField]
        private TMP_Text _lifesText;
        [SerializeField]
        private TMP_Text _moneyText;

        private LevelController _levelController;

        private void Start()
        {
            _levelController = LevelController.Instance;
        }

        private void Update()
        {
            _roundsText.text = $"Волна: {_levelController.CurrentRound}/{_levelController.MaxRound}";
            _lifesText.text = $"Жизни: {_levelController.CurrentLifes}/{_levelController.MaxLifes}";
            _moneyText.text = $"Монеты: {_levelController.Money}";
        }
    }
}