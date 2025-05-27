
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.StaticData
{
    [CreateAssetMenu(fileName = "Character List", menuName = "Data/CharacterList")]
    public class CharacterList : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characters;

        public List<CharacterData> Characters => new(_characters);
    }
}