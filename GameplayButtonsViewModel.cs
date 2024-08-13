using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
using System;

public class GameplayButtonsViewModel : AbstractViewModel
{
    protected override Type Window => typeof(GameplayButtonsView);

    public GameplayButtonsViewModel(IWindowFsm windowFsm) : base(windowFsm)
    {

    }

    public override void InvokeClose()
    {
        _windowFsm.CloseWindow(Window);
    }

    public override void InvokeOpen()
    {
        _windowFsm.OpenWindow(Window);
    }

    public void OpenTestingToolsWindow()
    {
        InvokeClose();
        _windowFsm.OpenWindow(typeof(TestingToolsView), true);
    }

    public void OpenMenuPauseWindow()
    {
        InvokeClose();
        //_windowFsm.OpenWindow(typeof(MenuPauseView), true);
    }
}
