using System;
using UnityEngine;

namespace SmugRagGames.Player.Movement.Input
{
    public class PlayerMovementInput
    {
        private GameControls _input;

        #region Cases

        public bool EnteredMove { get; private set; }
        public bool EnteredRun { get; private set; }
        public bool EnteredJump { get; private set; }

        public bool EnteredCrouch { get; private set; }

        #endregion Cases

        #region Actions

        public event Action OnEnteredCrouchEvent;
        public event Action OnEnteredJumpEvent;

        #endregion Actions

        #region Setup

        public void Setup(GameControls input)
        {
            _input = input;

            //Events//
            _input.Player.Crouch.performed += _ => InvokeOnEnteredCrouch();
            _input.Player.Crouch.canceled += _ => ChangeEnteredCrouchState(false);

            _input.Player.Jump.performed += _ => InvokeOnEnteredJump();
            _input.Player.Jump.canceled += _ => ChangeEnteredJumpState(false);
        }

        #endregion Setup

        #region Private methodes

        private void InvokeOnEnteredCrouch()
        {
            OnEnteredCrouchEvent?.Invoke();
            ChangeEnteredCrouchState(true);
        }

        private void InvokeOnEnteredJump()
        {
            OnEnteredJumpEvent?.Invoke();
            ChangeEnteredJumpState(true);
        }

        private void ChangeEnteredCrouchState(bool newState)
        {
            EnteredCrouch = newState;
        }

        private void ChangeEnteredJumpState(bool newState)
        {
            EnteredJump = newState;
        }

        #endregion Private methodes

        #region Public methodes

        public void UpdateData()
        {
            EnteredMove = _input.Player.Move.IsPressed();
            EnteredRun = _input.Player.Run.IsPressed();
        }

        public GameControls GetGameControls()
        {
            return _input;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public Vector2 GetValueMove()
        {
            return _input.Player.Move.ReadValue<Vector2>();
        }

        #endregion Public methodes
    }
}