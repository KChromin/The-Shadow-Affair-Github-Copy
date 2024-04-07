using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultInput
    {
        private GameControls _input;

        #region Public Variables

        public bool InputMove { get; private set; }
        public bool InputRun { get; private set; }
        public bool InputJump { get; private set; }

        public event Action OnInputCrouchEvent;
        public event Action OnInputJumpEvent;

        #endregion Public Variables

        #region Setup

        public void Setup(GameControls input)
        {
            _input = input;

            //Setup input events//
            _input.Default.Crouch.performed += _ => InvokeInputCrouch();
            
            _input.Default.Jump.performed += _ => OnJumpPerform();
            _input.Default.Jump.canceled += _ => OnJumpCancel();
        }

        #endregion Setup

        #region Public methodes

        public GameControls GetGameControls()
        {
            return _input;
        }

        public void UpdateInputStates()
        {
            //Move//
            InputMove = _input.Default.Movement.IsPressed();

            //Run//
            InputRun = _input.Default.Run.IsPressed();
        }

        public Vector2 GetInputMovement()
        {
            return _input.Default.Movement.ReadValue<Vector2>();
        }

        #endregion Public methodes

        #region Private methodes

        private void InvokeInputCrouch()
        {
            OnInputCrouchEvent?.Invoke();
        }

        private void InvokeInputJump()
        {
            OnInputJumpEvent?.Invoke();
        }

        private void OnJumpPerform()
        {
            InvokeInputJump();

            InputJump = true;
        }

        private void OnJumpCancel()
        {
            InputJump = false;
        }

        #endregion Private methodes
    }
}