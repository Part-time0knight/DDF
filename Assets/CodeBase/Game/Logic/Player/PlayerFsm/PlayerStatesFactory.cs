using Game.Domain.Factories.GameFsm;
using Zenject;

namespace Game.Logic.Player.PlayerFsm
{
    public class PlayerStatesFactory : StatesFactory
    {
        public PlayerStatesFactory(DiContainer container) : base(container)
        {
        }
    }
}