using Game.Domain.Factories.GameFsm;
using Zenject;

namespace Game.Logic.Enemy.EnemyFsm
{
    public class EnemyStatesFactory : StatesFactory
    {
        public EnemyStatesFactory(DiContainer container) : base(container)
        {
        }

        
    }
}