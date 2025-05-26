using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Misc;
using Game.Presentation.View;

namespace Game.Infrastructure.States.Gameplay
{
    public class Load : IState
    {
        private readonly IWindowFsm _windowFsm;
        private readonly SceneLoader _sceneLoader;

        public Load(IWindowFsm windowFsm,
            SceneLoader sceneLoader)
        {
            _windowFsm = windowFsm;
            _sceneLoader = sceneLoader;
        }

        public void OnEnter()
        {
            _sceneLoader.LoadMenu();
            _windowFsm.OpenWindow(typeof(LoadView), true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}