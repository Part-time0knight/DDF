using Game.Logic.Misc;
using Game.Logic.Player;
using Game.Logic.Weapon;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [field: SerializeField] public PlayerSettings Player { get; private set; }
        [field: SerializeField] public ProjectileSettings Projectile { get; private set; }

        [Serializable]
        public class PlayerSettings
        {
            public PlayerShootHandler.PlayerSettings PlayerWeapon;
            public PlayerMove.PlayerSettings PlayerMove;
            public PlayerDamageHandler.PlayerSettings PlayerHits;
        }

        [Serializable]
        public class ProjectileSettings
        {
            public BulletMove.BulletSettngs BulletMove;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Player.PlayerWeapon).AsSingle();
            Container.BindInstance(Player.PlayerMove).AsSingle();
            Container.BindInstance(Player.PlayerHits).AsSingle();

            Container.BindInstance(Projectile.BulletMove).AsSingle();
        }
    }
}