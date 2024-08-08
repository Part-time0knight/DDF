using Game.Presentation.View;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.InteractiveObject;
using System;
using Game.Logic.Weapon;
using Unity.VisualScripting;
using Game.Logic.Player;


namespace Game.Presentation.ViewModel
{
    public class PlayerReloadViewModel : AbstractViewModel
    {
        public Action<float> InvokeUpdate;

        public Action<bool> InvokeActive;

        protected override Type Window => typeof(PlayerReloadView);

        private readonly PlayerShootHandler.PlayerSettings _playerSettings;

        public PlayerReloadViewModel(IWindowFsm windowFsm, PlayerShootHandler.PlayerSettings playerSettings) : base(windowFsm)
        {
            _playerSettings = playerSettings;
            
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            _playerSettings.InvokeTimeUpdate += ReloadUpdate;
            ReloadUpdate();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            _playerSettings.InvokeTimeUpdate -= ReloadUpdate;
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
            InvokeUpdate?.Invoke(_playerSettings.TimeToAttack / _playerSettings.CurrentAttackDelay);
            InvokeActive?.Invoke(_playerSettings.TimeToAttack != 0f);
        }
    }
}