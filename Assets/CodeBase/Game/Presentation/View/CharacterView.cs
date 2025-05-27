using Core.MVVM.View;
using DG.Tweening;
using Game.Domain.Dto;
using Game.Presentation.Elements;
using Game.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Presentation.View
{
    public class CharacterView : AbstractPayloadView<CharacterViewModel>
    {
        [SerializeField] private Settings _settings;

        private readonly List<Image> _imageList = new();

        private Pool _imagePool;
        private float _baseScrollValue;



        [Inject]
        private void Construct(CharacterViewModel viewModel, Pool imagePool)
        {
            Construct(viewModel);
            _imagePool = imagePool;
            _viewModel.OnListUpdate += InvokeListUpdate;
            _viewModel.OnCharacterUpdate += InvokeCharacterUpdate;
            _viewModel.OnListMoveLeft += InvokeListMoveLeft;
            _viewModel.OnListMoveRight += InvokeListMoveRight;

            _settings.LeftArrow.onClick.AddListener(_viewModel.NavigationLeft);
            _settings.RightArrow.onClick.AddListener(_viewModel.NavigationRight);
            _settings.BackButton.onClick.AddListener(_viewModel.InvokeClose);
            _baseScrollValue = 0.5f;
        }

        protected override void OnDestroy()
        {
            _viewModel.OnListUpdate -= InvokeListUpdate;
            _viewModel.OnCharacterUpdate -= InvokeCharacterUpdate;
            _viewModel.OnListMoveLeft -= InvokeListMoveLeft;
            _viewModel.OnListMoveRight -= InvokeListMoveRight;

            _settings.LeftArrow.onClick.RemoveListener(_viewModel.NavigationLeft);
            _settings.RightArrow.onClick.RemoveListener(_viewModel.NavigationRight);
            _settings.BackButton.onClick.RemoveListener(_viewModel.InvokeClose);
            base.OnDestroy();
        }

        private void InvokeCharacterUpdate(CharacterDto dto)
        {
            _settings.CharacterWindow.UpdateCharacter(dto);
        }

        private void InvokeListUpdate(CharacterListDto dto)
        {
            while (_imageList.Count > 0)
            {
                _imagePool.Despawn(_imageList[0]);
                _imageList.RemoveAt(0);
            }

            for (int i = 0; i < dto.Characters.Count; i++)
            {
                var item = _imagePool.Spawn(dto.Characters[i].CharColor);
                item.transform.SetSiblingIndex(i);
                _imageList.Add(item);
            }
            _settings.ScrollList.ResetScroll(_baseScrollValue);
        }

        private void InvokeListMoveLeft(Action callback)
        {
            _settings.ScrollList.ScrollLeft(callback);
        }

        private void InvokeListMoveRight(Action callback)
        {
            _settings.ScrollList.ScrollRight(callback);
        }

        public class Pool : MemoryPool<Color, Image>
        {
            protected override void Reinitialize(Color color, Image item)
            {
                base.Reinitialize(color, item);
                item.color = color;
            }
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public ScrollList ScrollList { get; private set; }
            [field: SerializeField] public CharacterWindow CharacterWindow { get; private set; }
            [field: SerializeField] public Button LeftArrow { get; private set; }
            [field: SerializeField] public Button RightArrow { get; private set; }

            [field: SerializeField] public Button BackButton { get; private set; }
        }
    }
}