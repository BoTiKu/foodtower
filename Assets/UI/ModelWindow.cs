using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace TowerDefense
{
    public class ModelWindow : MonoSingleton<ModelWindow>
    {
        [SerializeField]
        private GameObject _window;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Button _okeyButton;
        [SerializeField]
        private TMP_Text _buttonLabel;

        public void Show(string info, string buttonLabel, UnityAction onClickOkeyButton)
        {
            _okeyButton.onClick.RemoveAllListeners();
            _okeyButton.onClick.AddListener(onClickOkeyButton);

            _text.text = info;
            _buttonLabel.text = buttonLabel;

            _window.SetActive(true);
        }

        public void Close() => _window.SetActive(false);
    }
}