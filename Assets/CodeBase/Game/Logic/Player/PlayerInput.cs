using System;
using UnityEngine;
using Zenject;

namespace Game.Logic.Player
{
    public class PlayerInput : ITickable
    {
        public event Action InvokeMoveButtonsDown;

        public event Action InvokeMoveButtonsUp;

        public event Action<float> InvokeMoveHorizontal;

        public event Action<float> InvokeMoveVertical;

        public event Action InvokeAttackButton;
        public event Action InvokeSpellButton;

        private bool OnMoveButtonDown()
            => Input.GetButtonDown("Vertical")
                    || Input.GetButtonDown("Horizontal");

        private bool OnMoveButtonUp()
            => Input.GetButtonUp("Vertical")
                    || Input.GetButtonUp("Horizontal");

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

            InvokeMoveHorizontal?.Invoke(OnMoveHorizontal());
            InvokeMoveVertical?.Invoke(OnMoveVertical());

            if (OnAttackButton())
                InvokeAttackButton?.Invoke();
            if (OnSpellButton())
                InvokeSpellButton?.Invoke();
        }
    }
}