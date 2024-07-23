using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerInput : ITickable, IFixedTickable
    {
        public event Action InvokeMoveButtonsDown;

        public event Action InvokeMoveButtonsUp;

        public event Action<float> InvokeMoveHorizontal;

        public event Action<float> InvokeMoveVertical;

        public event Action<Vector2> InvokeMove;

        public event Action InvokeAttackButton;
        public event Action InvokeSpellButton;

        private bool _isHorizontal;
        private bool _isVertical;

        public float Horizontal => OnMoveHorizontal();
        public float Vertical => OnMoveVertical();

        public Vector2 MousePosition()
            => Camera.main.ScreenToViewportPoint(
                new(Input.mousePosition.x,
                    Input.mousePosition.y,
                    Camera.main.nearClipPlane));

        public bool IsMoveButtonPress 
        { 
            get => Input.GetButton("Vertical")
                || Input.GetButton("Horizontal");
        }

        private bool OnMoveButtonDown()
            => Input.GetButtonDown("Vertical")
                || Input.GetButtonDown("Horizontal");

        private bool OnMoveButtonUp()
            => (Input.GetButtonUp("Vertical")
                || Input.GetButtonUp("Horizontal"))
                && !IsMoveButtonPress;

        private float OnMoveVertical()
            => Input.GetAxis("Vertical");

        private float OnMoveHorizontal()
            => Input.GetAxis("Horizontal");

        private bool OnAttackButton()
            => Input.GetButton("Fire1");

        private bool OnSpellButton()
            => Input.GetButton("Spell");

        public void Tick()
        {
            if (OnMoveButtonDown())
                InvokeMoveButtonsDown?.Invoke();
            if (OnMoveButtonUp())
                InvokeMoveButtonsUp?.Invoke();

            if (OnAttackButton())
                InvokeAttackButton?.Invoke();
            if (OnSpellButton())
                InvokeSpellButton?.Invoke();
        }

        public void FixedTick()
        {
            _isHorizontal = OnMoveHorizontal() != 0;
            _isVertical = OnMoveVertical() != 0;
            if (_isHorizontal)
                InvokeMoveHorizontal?.
                    Invoke(OnMoveHorizontal());
            if (_isVertical)
                InvokeMoveVertical?.
                    Invoke(OnMoveVertical());

            //if (_isHorizontal || _isVertical)
            InvokeMove?.Invoke(new(OnMoveHorizontal(), OnMoveVertical()));
        }
    }
}