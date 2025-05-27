using Game.Logic.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Logic.Character
{
    public class CharacterService : IInitializable
    {
        public event Action OnUpdate;

        private readonly List<CharacterData> _characterData;
        private readonly List<Character> _characters = new();
        
        private Character _currentCharacter;
        
        public List<Character> Characters => _characters;

        public Character CurrentCharacter 
        {
            get => _currentCharacter;
            set => _currentCharacter = value;
        }

        public CharacterService(CharacterList characterList) 
        {
            _characterData = characterList.Characters;
        }

        public void Initialize()
        {
            foreach (var character in _characterData)
            {
                _characters.Add(new(character));
            }
            _currentCharacter = _characters[0];
        }

        public void SetCharacter(int index)
        {
            _currentCharacter = _characters[index];
            OnUpdate?.Invoke();
        }

        public struct Character
        {
            public Color CharacterColor;
            public float CurrentExperience;
            public float MaxExperience;
            public int Id;
            public string Name;

            public Character(CharacterData data)
            {
                CharacterColor = data.Color;
                CurrentExperience = data.CurrentExperience;
                MaxExperience = data.MaxExperience;
                Id = data.Index;
                Name = data.Name;
            }
        }
    }
}