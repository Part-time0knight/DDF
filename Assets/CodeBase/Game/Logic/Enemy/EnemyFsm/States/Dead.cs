using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Enemy.EnemyFsm.States
{
    public class Dead : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly Rigidbody2D _body;
        private List<Collider2D> _colliders = new();

        public Dead(IGameStateMachine stateMachine,
            Rigidbody2D body) 
        { 
            _stateMachine = stateMachine;
            _body = body;
        }


        public void OnEnter()
        {
            _body.GetAttachedColliders(_colliders);
            _colliders.ForEach((collider) => collider.enabled = false);
        }

        public void OnExit()
        {
            _colliders.ForEach((collider) => collider.enabled = true);
            _stateMachine.Enter<Run>();
        }
    }
}