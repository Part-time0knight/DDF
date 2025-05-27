using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Infrastructure.States.MainMenu
{
    public class MainMenuState : IState
    {
        private readonly IWindowFsm _windowFsm;

        public MainMenuState(IWindowFsm windowFsm) 
        {
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            _windowFsm.OpenWindow(typeof(MainMenuView), true);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow();
        }
    }
}