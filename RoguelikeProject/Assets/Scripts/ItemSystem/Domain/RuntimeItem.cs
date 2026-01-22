using System.Collections.Generic;
using ItemSystem.Infrastructure.Config;
using Player.Domain.Items.Player.Domain.Items;

namespace ItemSystem.Domain
{
    public class RuntimeItem
    {
        public ItemConfig Config { get; private set; }
        public int StackCount { get; private set; }

        private List<RuntimeItemEffect> _activeEffects = new();
        private PlayerContext _context;

        public RuntimeItem(ItemConfig config, PlayerContext context)
        {
            Config = config;
            _context = context;
            StackCount = 0;

            foreach (var effectConfig in config.Effects)
            {
                RuntimeItemEffect effect = effectConfig.CreateRuntimeEffect();
                effect.Initialize(_context);
                _activeEffects.Add(effect);
            }
        }

        public void AddStack()
        {
            StackCount++;
            if (StackCount == 1)
            {
                foreach (var effect in _activeEffects) effect.OnEquip(StackCount);
            }
            else
            {
                foreach (var effect in _activeEffects) effect.OnStackChanged(StackCount);
            }
        }

        public void RemoveStack()
        {
            if (StackCount <= 0) return;
            
            StackCount--;
            if (StackCount == 0)
            {
                foreach (var effect in _activeEffects) effect.OnUnequip();
            }
            else
            {
                foreach (var effect in _activeEffects) effect.OnStackChanged(StackCount);
            }
        }
    }
}