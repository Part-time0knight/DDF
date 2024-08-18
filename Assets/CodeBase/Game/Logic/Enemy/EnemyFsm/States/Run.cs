using Core.Infrastructure.GameFsm;
using Core.Infrastructure.GameFsm.States;

namespace Game.Logic.Enemy.EnemyFsm.States
{
    public class Run : IState
    {
        private readonly IGameStateMachine _stateMachine;

        private readonly EnemyMoveHandler _moveHandler;
        private readonly EnemyTickHandler _tickHandler;
        private readonly EnemyDamageHandler.EnemySettings _damageSettings;

        public Run(IGameStateMachine stateMachine,
            EnemyMoveHandler moveHandler,
            EnemyTickHandler tickHandler,
            EnemyDamageHandler.EnemySettings damageSettings)
        {
            _stateMachine = stateMachine;

            _moveHandler = moveHandler;
            _tickHandler = tickHandler;
            _damageSettings = damageSettings;
        }

        public void OnEnter()
        {
            _tickHandler.OnFixedTick += UpdateMove;
            _damageSettings.InvokeHitPointsChange += OnHit;
        }

        public void OnExit()
        {
            _tickHandler.OnFixedTick -= UpdateMove;
            _damageSettings.InvokeHitPointsChange -= OnHit;
            _moveHandler.Stop();
        }

        private void UpdateMove()
        {
            _moveHandler.MoveToPlayer();
        }

        private void OnHit()
        {
            if (_damageSettings.CurrentHits == 0f)
                _stateMachine.Enter<Dead>();
        }
    }
}