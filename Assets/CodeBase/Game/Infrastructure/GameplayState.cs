using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Infrastructure
{
    public class GameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;

        public GameplayState(IGameStateMachine stateMachine, IWindowFsm windowFsm, IWindowResolve windowResolve)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            WindowResolve();
        }

        public void OnExit()
        {

        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<TestingToolsView>();
        }
    }
}