using DG.Tweening;
using Game.Domain.Dto;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Presentation.Elements
{
    public class CharacterWindow : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        private Sequence _sequence;

        public void UpdateCharacter(CharacterDto dto)
        {
            _settings.NameText.text = dto.Name;
            _settings.ExperienceText.text = dto.Experience;
            Cleaner();
            _sequence = DOTween.Sequence();

            _sequence
                .Join(
                    _settings
                    .ExperienceFill
                    .DOFillAmount(dto.ExperienceRatio, _settings.AnimationDuration)
                ).Join(
                    _settings
                    .CharacterImage
                    .DOColor(dto.CharColor, _settings.AnimationDuration)
                ).Play();
        }

        private void Cleaner()
        {
            if (_sequence == null) return;
            _sequence.Kill();
            _sequence = null;
        }

        private void OnDisable()
        {
            Cleaner();
        }


        [Serializable]
        public class Settings
        {
            [field: SerializeField] public TMP_Text NameText { get; private set; }
            [field: SerializeField] public TMP_Text ExperienceText { get; private set; }
            [field: SerializeField] public Image ExperienceFill { get; private set; }
            [field: SerializeField] public Image CharacterImage { get; private set; }
            [field: SerializeField] public float AnimationDuration { get; private set; } = 0.1f;
        }
    }
}