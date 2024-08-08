using Game.Domain.Factories;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallFactory();
    }

    private void InstallFactory()
    {
        Container
            .BindInterfacesAndSelfTo<StatesFactory>()
            .AsSingle()
            .NonLazy();
    }
}