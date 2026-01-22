using System.Collections.Generic;
using ItemSystem.Infrastructure.Config;
using Player.Domain.Items.Player.Domain.Items;

namespace ItemSystem.Domain
{
    public class InventoryController
    {
        private readonly Dictionary<string, RuntimeItem> _items = new();
        private readonly PlayerContext _context;

        public InventoryController(PlayerContext context)
        {
            _context = context;
        }

        public void AddItem(ItemConfig config)
        {
            if (_items.TryGetValue(config.Id, out var runtimeItem))
            {
                runtimeItem.AddStack();
            }
            else
            {
                var newItem = new RuntimeItem(config, _context);
                _items.Add(config.Id, newItem);
                newItem.AddStack();
            }
        }

        public void RemoveItem(ItemConfig config)
        {
            if (_items.TryGetValue(config.Id, out var runtimeItem))
            {
                runtimeItem.RemoveStack();
                if (runtimeItem.StackCount <= 0)
                {
                    _items.Remove(config.Id);
                }
            }
        }
    }
}