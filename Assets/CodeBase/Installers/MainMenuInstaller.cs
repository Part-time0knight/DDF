using Game.Infrastructure;
using Core.MVVM.Windows;
using Game.Presentation.ViewModel;
using Zenject;
using Game.Domain.Factories.GameFsm;
using Game.Logic.Misc;
using Game.Logic.Character;
using Game.Logic.Weapon;
using UnityEngine.UI;
using Game.Presentation.View;
using UnityEngine;
using System;


public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        InstallViewModel();
        InstallServices();
        InstallFactories();
        InstallPools();
    }

    private void InstallFactories()
    {
        Container
            .BindInterfacesAndSelfTo<StatesFactory>()
            .AsSingle()
            .NonLazy();
    }

    private void InstallPools()
    {
        Container.BindMemoryPool<Image, CharacterView.Pool>()
            .FromComponentInNewPrefab(_settings.CharacterIconPrefab)
            .UnderTransform(_settings.CharacterIconContainer); ;
    }

    private void InstallViewModel()
    {
        Container
            .BindInterfacesAndSelfTo<MainMenuViewModel>()
            .AsSingle()
            .NonLazy();
        Container
            .BindInterfacesAndSelfTo<CharacterViewModel>()
            .AsSingle()
            .NonLazy();
        Container
            .BindInterfacesAndSelfTo<LoadViewModel>()
            .AsSingle()
            .NonLazy();

    }

    private void InstallServices()
    {
        Container
            .BindInterfacesAndSelfTo<WindowFsm>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<MainMenuFsm>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<SceneLoader>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<CharacterService>()
            .AsSingle()
            .NonLazy();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Image CharacterIconPrefab { get; private set; }
        [field: SerializeField] public RectTransform CharacterIconContainer { get; private set; }
    }
}