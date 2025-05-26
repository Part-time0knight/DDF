using Core.Infrastructure.GameFsm;
using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Infrastructure.States.Gameplay;
using Game.Logic.Handlers;
using Game.Presentation.View;
using System;

public class MenuPauseViewModel : AbstractViewModel
{
    private readonly IPauseHandler _pause;
    private readonly IGameStateMachine _gameFsm;

    protected override Type Window => typeof(MenuPauseView);

    public MenuPauseViewModel(IWindowFsm windowFsm,
        IPauseHandler pause,
        IGameStateMachine gameFsm) : base(windowFsm)
    {
        _pause = pause;
        _gameFsm = gameFsm;
    }

    public override void InvokeClose()
    {
        _windowFsm.CloseWindow();
        _pause.Active = false;
    }

    public override void InvokeOpen()
    {
        _windowFsm.OpenWindow(Window, inHistory: true);
    }

    public void OpenSettings()
    {
        _windowFsm.OpenWindow(typeof(SettingsView), inHistory: true);
    }

    public void InvokeMenu()
    {
        _gameFsm.Enter<Load>();
    }
}
