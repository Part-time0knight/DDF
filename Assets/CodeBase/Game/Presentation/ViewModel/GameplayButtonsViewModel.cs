using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
using System;

namespace Game.Presentation.ViewModel
{
    public class GameplayButtonsViewModel : AbstractViewModel
    {
        protected override Type Window => typeof(GameplayButtonsView);

        public GameplayButtonsViewModel(IWindowFsm windowFsm) : base(windowFsm)
        {

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
            //_windowFsm.OpenWindow(typeof(MenuPauseView), true);
        }
    }
}