using Game.Logic.Player;
using Game.Logic.Player.Animation;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AnimationFsm>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }
    }
}