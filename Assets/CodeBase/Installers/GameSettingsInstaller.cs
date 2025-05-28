using Game.Logic.StaticData;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {

        [field: SerializeField] public CharacterList CharacterList { get; private set; }


        public override void InstallBindings()
        {
            Container.BindInstance(CharacterList).AsSingle();
        }
    }
}