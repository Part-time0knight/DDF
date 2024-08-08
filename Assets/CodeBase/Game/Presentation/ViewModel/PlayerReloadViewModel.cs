using Game.Presentation.View;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using System;
using Game.Logic.Player;

namespace Game.Presentation.ViewModel
{
    public class PlayerReloadViewModel : AbstractViewModel
    {
        public Action<float> InvokeActive;

        protected override Type Window => typeof(PlayerReloadView);

        private readonly PlayerShootHandler.PlayerSettings _playerSettings;

        public PlayerReloadViewModel(IWindowFsm windowFsm, PlayerShootHandler.PlayerSettings playerSettings) : base(windowFsm)
        {
            _playerSettings = playerSettings;
            
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _playerSettings.InvokeShoot += ReloadUpdate;
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _playerSettings.InvokeShoot -= ReloadUpdate;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow(Window);
        }

        public override void InvokeOpen()
        {
            
            _windowFsm.OpenWindow(Window);

        }

        private void ReloadUpdate()
        {
            InvokeActive?.Invoke(_playerSettings.CurrentAttackDelay);
        }
    }
}