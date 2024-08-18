using Game.Logic.Enemy;
using Game.Logic.Enemy.EnemyFsm;
using System;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        InstallFactories();
        InstallFsm();
        InstallPlayerComponents();
    }

    private void InstallPlayerComponents()
    {
        Container.BindInstance(_settings.Body).AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyTickHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDamageHandler>().AsSingle();
    }

    private void InstallFsm()
    {

        Container
            .BindInterfacesAndSelfTo<EnemyFsm>()
            .AsSingle()
            .NonLazy();
    }

    private void InstallFactories()
    {
        Container
            .BindInterfacesAndSelfTo<EnemyStatesFactory>()
            .AsSingle()
            .NonLazy();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Rigidbody2D Body { get; private set; }
    }
}