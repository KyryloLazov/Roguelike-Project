using Player.Domain.PlayerStats;
using UniRx;
using UnityEngine;

namespace Player.Domain.PlayerStateMachine
{
    public class PlayerStateMachineData
    {
        public Vector2 MovementInput { get; set; }
        public ReactiveProperty<bool> DashRequested = new(false);
        public ReactiveProperty<bool> IsDashing = new();
        public ReactiveProperty<bool> IsStunned = new();
        public ReactiveProperty<bool> IsDead = new();
        public float DashCooldownTimer;
        public readonly IPlayerStatsProvider Stats;

        public PlayerStateMachineData(IPlayerStatsProvider stats)
        {
            Stats = stats;
        }
    }
}