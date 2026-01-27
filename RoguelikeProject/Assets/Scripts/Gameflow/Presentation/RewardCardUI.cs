using System;
using ItemSystem.Infrastructure.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameflow.Presentation
{
    public class RewardCardUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private Button _button;

        private ItemConfig _config;
        private Action<ItemConfig> _onClickCallback;

        public void Setup(ItemConfig config, Action<ItemConfig> onClick)
        {
            _config = config;
            _onClickCallback = onClick;

            if(config.Icon) _icon.sprite = config.Icon;
            _nameText.text = config.Name;
            _descText.text = config.Description;
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => _onClickCallback?.Invoke(_config));
        }
    }
}