using Game.Domain.Factories.GameFsm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerStatesFactory : GameStatesFactory
{
    public PlayerStatesFactory(DiContainer container) : base(container)
    {
    }
}
