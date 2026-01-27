using System.Collections.Generic;
using System.Linq;
using ItemSystem.Infrastructure.Config;
using UnityEngine;

namespace Gameflow.Domain
{
    public class RewardService
    {
        private readonly ItemDatabaseConfig _database;

        public RewardService(ItemDatabaseConfig database)
        {
            _database = database;
        }

        public List<ItemConfig> GetRandomRewards(int count)
        {
            return _database.AllItems
                .OrderBy(x => Random.value)
                .Take(count)
                .ToList();
        }
    }
}