using Game.Logic.Weapon;
using System.ComponentModel;
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
            BulletBuffer buffer = Container.InstantiatePrefabForComponent<BulletBuffer>(_bufferPrefab);

            Container.Bind<BulletBuffer>().FromInstance(buffer);

            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .FromComponentInNewPrefab(_bulletPrfab).AsSingle();
        }
    }
}