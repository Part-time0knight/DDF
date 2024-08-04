using Game.Logic.Player.Animation;
using Game.Logic.Player;
using Zenject;
using Game.Logic.Weapon;
using Game.Logic.Misc;
using UnityEngine;
using Game.Logic.InteractiveObject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        
        [SerializeField] private Transform _weapon;
        [SerializeField] private ObjectStats _player;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectStats>().FromInstance(_player).AsSingle();

            Container.BindInterfacesAndSelfTo<AnimationFsm>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShootHandler>().AsSingle().WithArguments(_weapon);
        }
    }
}