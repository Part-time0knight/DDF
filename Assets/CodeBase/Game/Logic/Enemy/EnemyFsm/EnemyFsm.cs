using Game.Logic.Enemy.EnemyFsm.States;
using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Zenject;

namespace Game.Logic.Enemy.EnemyFsm
{
    public class EnemyFsm : AbstractGameStateMachine, IInitializable
    {
        public EnemyFsm(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            StateResolve();
            Enter<Initialize>();
        }

        protected void StateResolve()
        {
            _states.Add(typeof(Initialize), _factory.Create<Initialize>());
            _states.Add(typeof(Run), _factory.Create<Run>());
            _states.Add(typeof(Dead), _factory.Create<Dead>());
        }
    }
}