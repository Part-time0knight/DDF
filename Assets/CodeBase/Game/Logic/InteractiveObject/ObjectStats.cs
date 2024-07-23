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

        protected int _currentHits;

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

        public ObjectStats()
        {
            _currentHits = _hits;
        }
    }
}