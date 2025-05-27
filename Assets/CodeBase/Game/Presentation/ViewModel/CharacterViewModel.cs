using Core.MVVM.ViewModel;
using Core.MVVM.Windows;
using Game.Domain.Dto;
using Game.Logic.Character;
using Game.Presentation.View;
using System;
using Zenject;

namespace Game.Presentation.ViewModel
{
    public class CharacterViewModel : AbstractViewModel, IInitializable
    {
        public const int IndexArrowLength = 5;

        public event Action<CharacterListDto> OnListUpdate;
        public event Action<CharacterDto> OnCharacterUpdate;

        public event Action<Action> OnListMoveLeft;
        public event Action<Action> OnListMoveRight;

        private readonly CharacterService _characterService;

        private readonly CharacterListDto _characterList = new();

        private readonly int[] _currentIndexes = new int[IndexArrowLength];

        private int _currentIndex = 0;
        private bool _moveLeft;

        protected override Type Window => typeof(CharacterView);
        public CharacterViewModel(IWindowFsm windowFsm,
            CharacterService characterService) : base(windowFsm)
        {
            _characterService = characterService;
        }

        public void Initialize()
        {
            for (int i = 0; i < _currentIndexes.Length; i++)
                _characterList.Characters.Add(new());
        }

        public void NavigationLeft()
        {
            int index = _currentIndex - 1;
            if (index < 0)
                index = _characterService.Characters.Count - 1;
            _moveLeft = true;
            Navigate(index);
        }

        public void NavigationRight()
        {
            int index = _currentIndex + 1;
            if (index >= _characterService.Characters.Count)
                index = 0;
            _moveLeft = false;
            Navigate(index);
        }

        public override void InvokeClose()
        {
            _windowFsm.CloseWindow();
        }

        public override void InvokeOpen()
        {
            _windowFsm.OpenWindow(Window, inHistory: true);
        }

        protected override void HandleOpenedWindow(Type uiWindow)
        {
            base.HandleOpenedWindow(uiWindow);
            if (uiWindow != Window) return;
            _characterService.OnUpdate += MoveList;
            UpdateList();
        }

        protected override void HandleClosedWindow(Type uiWindow)
        {
            base.HandleClosedWindow(uiWindow);
            if (uiWindow != Window) return;
            _characterService.OnUpdate -= MoveList;
        }

        private void Navigate(int newIndex)
        {
            _characterService.SetCharacter(newIndex);
        }

        private void MoveList()
        {
            if (_moveLeft)
                OnListMoveLeft?.Invoke(UpdateList);
            else
                OnListMoveRight?.Invoke(UpdateList);
        }

        private void UpdateList()
        {
            for (int i = 0; i < _characterService.Characters.Count && 
                _characterService.Characters.Count > 1; i++)
            {
                if (_characterService.Characters[i].Id
                    == _characterService.CurrentCharacter.Id)
                {
                    _currentIndexes[2] = i;
                    _currentIndex = i;
                    break;
                }
            }

            for (int i = 1; i >= 0; i--)
            {
                _currentIndexes[i] = _currentIndexes[i + 1] - 1;
                if (_currentIndexes[i] < 0)
                    _currentIndexes[i] = _characterService.Characters.Count - 1;
            }

            for (int i = 3; i < _currentIndexes.Length; i++)
            {
                _currentIndexes[i] = _currentIndexes[i - 1] + 1;
                if (_currentIndexes[i] >= _characterService.Characters.Count)
                    _currentIndexes[i] = 0;
            }

            for (int i = 0; i < _currentIndexes.Length; i++)
            {
                _characterList.Characters[i].CharColor =
                    _characterService.Characters[_currentIndexes[i]].CharacterColor;

                _characterList.Characters[i].ExperienceRatio =
                    _characterService.Characters[_currentIndexes[i]].CurrentExperience /
                    _characterService.Characters[_currentIndexes[i]].MaxExperience;

                _characterList.Characters[i].Experience =
                    _characterService.Characters[_currentIndexes[i]].CurrentExperience + "/" +
                    _characterService.Characters[_currentIndexes[i]].MaxExperience;

                _characterList.Characters[i].Name =
                    _characterService.Characters[_currentIndexes[i]].Name;
            }

            OnCharacterUpdate?.Invoke(_characterList.Characters[2]);

            OnListUpdate?.Invoke(_characterList);
        }
    }
}