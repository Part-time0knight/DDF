using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Presentation.Elements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeWindow : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private Tween _tween;
        private CanvasGroup _group;

        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
        }

        public void FadeOn(Action callback)
        {
            Clear();
            _tween = _group?
                .DOFade(1f, _settings.FadeDuration)
                .OnComplete(callback.Invoke);
        }

        public void FadeOff(Action callback)
        {
            Clear();
            _tween = _group
                .DOFade(0f, _settings.FadeDuration)
                .SetDelay(_settings.FadeOffDelay)
                .OnComplete(callback.Invoke);

        }

        private void OnDisable()
        {
            Clear();
        }

        private void Clear()
        {
            if (_tween == null) return;
            _tween.Kill();
            _tween = null;
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float FadeDuration { get; private set; } = 0.2f;
            [field: SerializeField] public float FadeOffDelay { get; private set; } = 0.2f;
        }
    }
}