using Core.Domain.Factories;
using Core.Infrastructure.GameFsm;
using Game.Logic.Player.PlayerFsm.States;
using Zenject;

namespace Game.Logic.Player.PlayerFsm
{
    public class PlayerFsm : AbstractGameStateMachine, IInitializable
    {
        public PlayerFsm(IStatesFactory factory) : base(factory)
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
            _states.Add(typeof(Idle), _factory.Create<Idle>());
            _states.Add(typeof(Run), _factory.Create<Run>());
            //_states.Add(typeof(Attack), _factory.Create<Attack>());
            _states.Add(typeof(Dead), _factory.Create<Dead>());
        }
    }
}