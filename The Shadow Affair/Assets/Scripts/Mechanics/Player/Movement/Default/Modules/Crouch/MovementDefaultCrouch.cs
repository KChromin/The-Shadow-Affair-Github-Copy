using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCrouch
    {
        #region Classes

        private MovementDefaultInput _input;
        private MovementDefaultPossibilitiesData _possibilities;
        private MovementDefaultCheckers _checkers;
        private MovementDefaultCasesData _casesData;
        private MovementDefaultActions _actions;

        #endregion Classes

        public void Setup(MovementDefaultInput input, MovementDefaultPossibilitiesData possibilities,  MovementDefaultCasesData casesData, MovementDefaultActions actions, MovementDefaultCheckers checkers)
        {
            _input = input;
            _possibilities = possibilities;
            _casesData = casesData;
            _actions = actions;
            _checkers = checkers;

            SubscribeToEvents();
        }

        private void SetCrouch()
        {
            _casesData.isCrouching = GetNewCrouchState();
        }

        private bool GetNewCrouchState()
        {
            if (_casesData.isCrouching)
            {
                //If celling is hit, dont change state//
                return _checkers.HitCelling();
            }

            if (!_possibilities.canCrouch)
            {
                return false;
            }

            return true;
        }

        #region Action subscriptions

        private void SubscribeToEvents()
        {
            _input.OnInputCrouchEvent += SetCrouch;
            _actions.OnFromCrouchToStandAfterJumpAction += SetCrouch;
            _actions.OnCrouchToRunAction += SetCrouch;
        }

        public void UnsubscribeFromEvents()
        {
            _input.OnInputCrouchEvent -= SetCrouch;
            _actions.OnFromCrouchToStandAfterJumpAction -= SetCrouch;
            _actions.OnCrouchToRunAction -= SetCrouch;
        }

        #endregion Action subscriptions
    }
}