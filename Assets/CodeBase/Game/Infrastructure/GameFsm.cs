using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Infrastructure.States.Gameplay;
using Zenject;

namespace Game.Infrastructure
{
    public class GameFsm : AbstractGameStateMachine, IInitializable
    {
        public GameFsm(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            StateResolve();
            Enter<GameplayState>();
        }

        private void StateResolve()
        {
            _states.Add(typeof(GameplayState), _factory.Create<GameplayState>());
            _states.Add(typeof(Load), _factory.Create<Load>());
        }
    }
}