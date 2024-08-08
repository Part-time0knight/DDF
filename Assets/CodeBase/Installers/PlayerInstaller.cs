using Game.Logic.Player.Animation;
using Game.Logic.Player;
using Zenject;
using UnityEngine;
using System;
using Game.Domain.Factories.GameFsm;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallFactory();
            InstallPlayerComponents();
        }

        private void InstallFactory()
        {
            Container
                .BindInterfacesAndSelfTo<AnimationStatesFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallPlayerComponents()
        {
            Container.BindInstance(_settings.Weapon).AsSingle();
            Container.BindInstance(_settings.Animation).AsSingle();
            Container.BindInstance(_settings.Body).AsSingle();

            Container.BindInterfacesAndSelfTo<AnimationFsm>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMove>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public Transform Weapon { get; private set; }
            [field: SerializeField] public UnitAnimationExtension Animation { get; private set; }
            [field: SerializeField] public Rigidbody2D Body { get; private set; }
        }
    }
}