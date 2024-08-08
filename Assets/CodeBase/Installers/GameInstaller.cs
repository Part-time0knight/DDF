using Core.MVVM.Windows;
using Game.Logic.Player.PlayerFsm;
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
            InstallPools();
            InstallViewModel();
            InstallService();
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
                .BindInterfacesAndSelfTo<PlayerReloadViewModel>()
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
                .BindInterfacesAndSelfTo<PlayerStateMachine>()
                .AsSingle()
                .NonLazy();

            
        }
    }
}