using System;
using ItemSystem.Domain;
using ItemSystem.Infrastructure.Config;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Debug
{
    public class AddItemButton : MonoBehaviour
    {
        [SerializeField] private ItemConfig _itemConfig;
        
        [Inject] private InventoryController _inventoryController;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => _inventoryController.AddItem(_itemConfig));
        }

        private void AddItem()
        {
            _inventoryController.AddItem(_itemConfig);
        }
    }
}