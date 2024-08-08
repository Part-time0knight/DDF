using Game.Domain.Factories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStatesFactory : StatesFactory
{
    public PlayerStatesFactory(DiContainer container) : base(container)
    {
    }
}
