using Player.Domain.Events;
using Player.Domain.PlayerStateMachine;
using Player.Domain.PlayerStats;
using Player.Domain.PlayerStatus;
using Weapons.Domain;

namespace Player.Domain.Items
{
    namespace Player.Domain.Items
    {
        public class PlayerContext
        {
            public readonly IPlayerStatsMutable Stats;
            public readonly PlayerStatusModel Health;
            public readonly PlayerEvents Events;
            public readonly PlayerWeaponController Weapons;
            public readonly PlayerStateMachineData StateData;

            public PlayerContext(
                IPlayerStatsMutable stats, 
                PlayerStatusModel health, 
                PlayerEvents events, 
                PlayerWeaponController weapons,
                PlayerStateMachineData stateData)
            {
                Stats = stats;
                Health = health;
                Events = events;
                Weapons = weapons;
                StateData = stateData;
            }
        }
    }
}