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

        [Header("ToolTip")]
        [SerializeField]
        private GameObject _toolTip;
        [SerializeField]
        private TMP_Text _toolTipText;
        public void Show(string info, string buttonLabel, UnityAction onClickOkeyButton)
        {
            _okeyButton.onClick.RemoveAllListeners();
            _okeyButton.onClick.AddListener(onClickOkeyButton);
            _okeyButton.onClick.AddListener(() => AudioController.Instance.PlayClick());

            _text.text = info;
            _buttonLabel.text = buttonLabel;

            _window.SetActive(true);
        }

        public void ShowToolTip(string title, Vector3 position)
        {
            _toolTip.transform.position = position;
            _toolTipText.text = title;
            _toolTip.SetActive(true);
        }

        public void CloseToolTip() => _toolTip.SetActive(false);

        public void Close() => _window.SetActive(false);
    }
}