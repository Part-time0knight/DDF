using UnityEngine;

namespace Game.Logic.StaticData
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        [field: SerializeField] public float CurrentExperience { get; private set; }
        [field: SerializeField] public float MaxExperience { get; private set; }
    }
}