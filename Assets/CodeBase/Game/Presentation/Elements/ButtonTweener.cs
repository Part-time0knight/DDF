using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.Elements
{
    [RequireComponent(typeof(Button))]
    public class ButtonTweener : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private Button _button;
        private Vector3 originalScale;
        private Tween _tween;

        private void Awake()
        {
            _button = GetComponent<Button>();
            originalScale = transform.localScale;
        }

        private void OnDestroy()
        {
            Clear();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PlayBounceAnimation);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlayBounceAnimation);
        }

        private void PlayBounceAnimation()
        {
            Clear();

            _tween = transform.DOPunchScale(originalScale * _settings.BounceScale,
                _settings.BounceDuration, 
                _settings.BounceVibrato, 
                _settings.BounceElasticity)
                .SetEase(Ease.OutQuad);
        }

        private void Clear()
        {
            transform.localScale = originalScale;
            if (_tween == null) return;
            _tween.Kill();
            _tween = null;
            
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField] public float BounceDuration { get; private set; } = 0.4f;
            [field: SerializeField] public float BounceScale { get; private set; } = 0.1f;
            [field: SerializeField] public int BounceVibrato { get; private set; } = 10;
            [field: SerializeField] public float BounceElasticity { get; private set; } = 1f;
        }
    }
}