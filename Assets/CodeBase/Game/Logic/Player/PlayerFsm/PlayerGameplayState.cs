using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using Core.MVVM.Windows;
using Game.Presentation.View;

namespace Game.Logic.Player.PlayerFsm
{
    public class PlayerGameplayState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowResolve _windowResolve;
        private readonly IWindowFsm _windowFsm;

        public PlayerGameplayState(IGameStateMachine stateMachine, IWindowFsm windowFsm, IWindowResolve windowResolve)
        {
            _stateMachine = stateMachine;
            _windowResolve = windowResolve;
            _windowFsm = windowFsm;
        }

        public void OnEnter()
        {
            WindowResolve();
            _windowFsm.OpenWindow(typeof(PlayerReloadView), false);
        }

        public void OnExit()
        {
            _windowFsm.CloseWindow(typeof(PlayerReloadView));
        }

        private void WindowResolve()
        {
            _windowResolve.CleanUp();
            _windowResolve.Set<PlayerReloadView>();
        }
    }
}