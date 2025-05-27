using Core.Data.Dto;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Domain.Dto
{
    public class CharacterDto : IDto
    {
        public Color CharColor { get; set; }
        public float ExperienceRatio { get; set; }
        public string Experience { get; set; }
        public string Name { get; set; }
    }

    public class CharacterListDto : IDto
    {
        public List<CharacterDto> Characters { get; set; } = new();

    }
}