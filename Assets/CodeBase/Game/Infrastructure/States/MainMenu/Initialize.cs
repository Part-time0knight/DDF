using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Infrastructure.States.MainMenu
{
    public class Initialize : IState
    {
        private readonly IWindowResolve _windowResolve;
        private readonly IGameStateMachine _fsm;

        public Initialize(IWindowResolve windowResolve,
            IGameStateMachine fsm)
        { 
            _windowResolve = windowResolve;
            _fsm = fsm;
        }

        public void OnEnter()
        {
            WindowResolver();
            _fsm.Enter<MainMenuState>();

        }

        public void OnExit()
        {
        }

        private void WindowResolver()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<MainMenuView>();
            _windowResolve.Set<CharacterView>();
            _windowResolve.Set<SettingsView>();
            _windowResolve.Set<LoadView>();
        }
    }
}