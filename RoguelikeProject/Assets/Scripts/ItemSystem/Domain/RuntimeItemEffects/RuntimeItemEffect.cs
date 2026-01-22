using System;
using Player.Domain.Items.Player.Domain.Items;

namespace ItemSystem.Domain
{
    public abstract class RuntimeItemEffect : IDisposable
    {
        protected PlayerContext Context;

        public void Initialize(PlayerContext context)
        {
            Context = context;
        }

        public abstract void OnEquip(int stackCount);
        public abstract void OnStackChanged(int newStackCount);
        public abstract void OnUnequip();
        
        public virtual void Dispose() { }
    }
}