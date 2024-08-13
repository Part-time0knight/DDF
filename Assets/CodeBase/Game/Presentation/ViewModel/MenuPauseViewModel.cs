using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Presentation.View;
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
        _windowFsm.OpenWindow(Window, inHistory: true);
    }

    public void OpenSettings()
    {
        _windowFsm.OpenWindow(typeof(SettingsView), inHistory: true);
    }
}
