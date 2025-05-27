using DG.Tweening;
using Game.Presentation.ViewModel;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.Elements
{
    public class ScrollList : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private Action _animationCallback;

        public void ScrollLeft(Action callback)
        {
            Scroll(callback, -1);
        }

        public void ScrollRight(Action callback)
        {
            Scroll(callback, 1);
        }

        public void ResetScroll(float normalPos)
        {
            _settings.ScrollRect.horizontalNormalizedPosition = normalPos;
        }

        private void Scroll(Action callback, int direction = 1)
        {
            Cleaner();
            float step = GetStep();
            float pos = _settings.ScrollRect.horizontalNormalizedPosition + step * direction;

            _animationCallback = callback;
            _settings.CanvasGroup.blocksRaycasts = false;

            _settings
                .ScrollRect
                .DOHorizontalNormalizedPos(pos, _settings.ScrollDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(InvokeAnimationEnd);
        }

        private float GetStep()
            => (0.5f + 1f / (CharacterViewModel.IndexArrowLength - 1f))
                / ((CharacterViewModel.IndexArrowLength - 1f) / 2f);

        private void InvokeAnimationEnd()
        {
            _settings.CanvasGroup.blocksRaycasts = true;
            _animationCallback?.Invoke();
        }


        private void Cleaner()
        {
            _settings.ScrollRect.DOKill();
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_settings.ScrollRect.content);
        }

        private void OnDisable()
        {
            Cleaner();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
            [field: SerializeField] public ScrollRect ScrollRect { get; private set; }

            [field: SerializeField] public float ScrollDuration { get; private set; } = 0.1f;
        }
    }
}