using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using System;

public class MenuPauseViewModel : AbstractViewModel
{

    protected override Type Window => typeof(MenuPauseView);

    public MenuPauseViewModel(IWindowFsm windowFsm) : base(windowFsm)
    {

    }

    public override void InvokeClose()
    {
        _windowFsm.CloseWindow();
    }

    public override void InvokeOpen()
    {
        _windowFsm.OpenWindow(Window, true);
    }
}
