using Core.Domain.Factories;
using Core.Infrastructure.GameFsm.States;
using Zenject;

namespace Game.Domain.Factories.GameFsm
{
    public class GameStatesFactory : IStatesFactory
    {
        private readonly DiContainer _container;

        public GameStatesFactory(DiContainer container) =>
            _container = container;

        public TState Create<TState>() where TState : class, IExitableState
        {
            TState state = _container.Instantiate<TState>();
            return state;
        }
    }
}
