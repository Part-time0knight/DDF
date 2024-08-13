using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
using Game.Infrastructure.Signals;
using Game.Logic.InteractiveObject;
using Game.Logic.Weapon;
using Game.Presentation.ViewModel;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BulletBuffer _bufferPrefab;
        [SerializeField] private Bullet _bulletPrfab;

        public override void InstallBindings()
        {
            InstallSignals();
            InstallFactory();
            InstallPools();
            InstallService();
            InstallViewModel();
            
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PauseSignal>();

        }

        private void InstallFactory()
        {
            Container
                .BindInterfacesAndSelfTo<StatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPools()
        {
            BulletBuffer buffer = Container.InstantiatePrefabForComponent<BulletBuffer>(_bufferPrefab);

            Container.Bind<BulletBuffer>().FromInstance(buffer).AsSingle();

            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_bulletPrfab);
        }

        private void InstallViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<TestingToolsViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<GameplayButtonsViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<MenuPauseViewModel>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<SettingsViewModel>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallService()
        {
            Container
                .BindInterfacesAndSelfTo<WindowFsm>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GameFsm>()
                .AsSingle()
                .NonLazy();


            Container.Bind<Pause>().AsSingle();
        }
    }
}