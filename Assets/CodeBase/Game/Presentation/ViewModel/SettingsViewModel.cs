using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using System;

public class SettingsViewModel : AbstractViewModel
{
    public event Action<bool> InvokeUpdate;


    public SettingsViewModel(IWindowFsm windowFsm) : base(windowFsm)
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

    public void ChangeSound(bool active)
    { }

    private void OnUpdate(bool active)
    {
        InvokeUpdate?.Invoke(active);
    }
}
