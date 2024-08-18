using Game.Logic.Handlers;
using Zenject;

namespace Game.Logic.Enemy
{
    public class EnemyHandler : UnitHandler
    {
        private EnemyDamageHandler _damageHandler;

        public override void MakeCollizion(int damage)
            => TakeDamage(damage);

        public void TakeDamage(int damage)
        {
            _damageHandler.TakeDamage(damage);
        }

        [Inject]
        private void Construct(EnemyDamageHandler damageHandler)
        {
            _damageHandler = damageHandler;
        }
    }
}