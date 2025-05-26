using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Infrastructure.States.MainMenu;
using Zenject;

namespace Game.Infrastructure
{
    public class MainMenuFsm : AbstractGameStateMachine, IInitializable
    {
        public MainMenuFsm(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            StateResolve();
            Enter<Initialize>();
        }

        private void StateResolve()
        {
            _states.Add(typeof(Initialize), _factory.Create<Initialize>());
            _states.Add(typeof(MainMenuState), _factory.Create<MainMenuState>());
            _states.Add(typeof(Exit), _factory.Create<Exit>());
        }
    }
}