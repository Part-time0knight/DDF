using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        private PlayerDamageHandler _damageHandler;

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(PlayerDamageHandler damageHandler)
        {
            _damageHandler = damageHandler;
        }
    }
}