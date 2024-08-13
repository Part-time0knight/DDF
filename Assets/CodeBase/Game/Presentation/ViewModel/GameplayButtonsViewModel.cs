using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Logic.InteractiveObject;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class GameplayButtonsViewModel : AbstractViewModel
    {
        private readonly Pause _pause;

        protected override Type Window => typeof(GameplayButtonsView);

        public GameplayButtonsViewModel(IWindowFsm windowFsm, Pause pause) : base(windowFsm)
        {
            _pause = pause;
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        public void OpenTestingToolsWindow()
        {
            _windowFsm.OpenWindow(typeof(TestingToolsView), inHistory: true);
        }

        public void OpenMenuPauseWindow()
        {
            _pause.Set(pause: true);
            _windowFsm.OpenWindow(typeof(MenuPauseView), inHistory: true);
        }
    }
}