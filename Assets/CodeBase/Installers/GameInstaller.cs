using Core.MVVM.Windows;
using Game.Domain.Factories.GameFsm;
using Game.Infrastructure;
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
            InstallFactory();
            InstallPools();
            InstallService();
            InstallViewModel();
            
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

            Container.Bind<BulletBuffer>().FromInstance(buffer);

            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_bulletPrfab).AsSingle();
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

            
        }
    }
}