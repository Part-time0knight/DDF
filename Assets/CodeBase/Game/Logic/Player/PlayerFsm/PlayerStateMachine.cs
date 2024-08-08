using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Zenject;

namespace Game.Logic.Player.PlayerFsm
{
    public class PlayerStateMachine : AbstractGameStateMachine, IInitializable
    {
        public PlayerStateMachine(IStatesFactory factory) : base(factory)
        {
        }

        public void Initialize()
        {
            //StateResolve();
            //Enter<PlayerGameplayState>();
        }

        private void StateResolve()
        {
            _states.Add(typeof(PlayerGameplayState), _factory.Create<PlayerGameplayState>());
        }
    }
}