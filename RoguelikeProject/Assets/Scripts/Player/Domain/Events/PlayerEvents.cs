using UniRx;

namespace Player.Domain.Events
{
    public class PlayerEvents
    {
        public readonly Subject<Unit> OnDashStarted = new();
        public readonly Subject<float> OnIdleTick = new(); 
        public readonly Subject<float> OnDamageTaken = new();
        public readonly Subject<Unit> OnShoot = new();
    }
}