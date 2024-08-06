using System;
using UnityEngine;

namespace Game.Logic.InteractiveObject
{
    [Serializable]
    public class ObjectStats
    {
        [SerializeField] protected int _hits;

        [SerializeField] protected int _damage;

        [SerializeField] protected float _speed;

        [SerializeField] protected float _attackDelay;

        protected int _currentHits;

        protected float _currentAttackDelay;

        public virtual int Hits 
        { 
            get => _hits;
            protected set => _hits = value;
        }

        public virtual int CurrentHits
        {
            get => _currentHits;
            set => _currentHits = value;
        }

        public virtual int Damage 
        {
            get => _damage;
            protected set => _damage = value;
        }

        public virtual float Speed
        {
            get => _speed;
            protected set => _speed = value;
        }

        public virtual float AttackDelay
        {
            get => _attackDelay;
            set => _attackDelay = value;
        }

        public virtual float CurrentAttackDelay
        {
            get => _currentAttackDelay;
            set => _currentAttackDelay = value;
        }

        public ObjectStats()
        {
            _currentHits = _hits;
            _currentAttackDelay = 0;
        }
    }
}