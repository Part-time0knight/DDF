using Game.Logic.Misc;
using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private Bullet _bulletPrfab;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<Bullet, Bullet.Pool>()
            .WithInitialSize(3).FromComponentInNewPrefab(_bulletPrfab);
    }
}