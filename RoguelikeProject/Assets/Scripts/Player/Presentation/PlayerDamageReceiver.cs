using Player.Domain.PlayerStatus;
using UnityEngine;
using Zenject;

namespace Player.Presentation
{
    public class PlayerDamageReceiver : MonoBehaviour, IDamageable
    {
        private PlayerStatusModel _statusModel;

        [Inject]
        public void Construct(PlayerStatusModel statusModel)
        {
            _statusModel = statusModel;
        }
        
        public void TakeDamage(float amount)
        {
            _statusModel.TakeDamage(amount);
        }
    }
}