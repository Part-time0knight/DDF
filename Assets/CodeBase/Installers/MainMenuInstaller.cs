using Game.Infrastructure;
using Core.MVVM.Windows;
using Game.Presentation.ViewModel;
using Zenject;
using Game.Domain.Factories.GameFsm;

public class MainMenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallViewModel();
        InstallServices();
        InstallFactory();
    }

    private void InstallFactory()
    {
        Container
            .BindInterfacesAndSelfTo<StatesFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void InstallViewModel()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuViewModel>()
            .AsSingle()
            .NonLazy();

    }

    private void InstallServices()
    {
        Container
            .BindInterfacesAndSelfTo<WindowFsm>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<MainMenuFsm>()
            .AsSingle()
            .NonLazy();
    }
}