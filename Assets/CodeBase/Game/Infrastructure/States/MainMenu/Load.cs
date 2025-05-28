using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Logic.Misc;
using Game.Presentation.View;

namespace Game.Infrastructure.States.MainMenu
{
    public class Load : IState
    {
        private readonly IWindowFsm _windowFsm;
        //private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly Timer _timer = new();

        public Load(IWindowFsm windowFsm,
            IGameStateMachine gameStateMachine)
        {
            _windowFsm = windowFsm;
            _gameStateMachine = gameStateMachine;
        }

        public void OnEnter()
        {
            _timer.Initialize(0.4f, _gameStateMachine.Enter<MainMenuState>).Play();
            _windowFsm.OpenWindow(typeof(LoadView), true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}