using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultRun
    {
        #region Classes

        private MovementDefaultInput _input;
        private MovementDefaultPossibilitiesData _possibilities;
        private MovementDefaultCheckers _checkers;
        private MovementDefaultActions _actions;
        private MovementDefaultCasesData _cases;

        #endregion Classes

        public void Setup(MovementDefaultInput input, MovementDefaultPossibilitiesData possibilities, MovementDefaultCheckers checkers, MovementDefaultCasesData cases, MovementDefaultActions actions)
        {
            _input = input;
            _possibilities = possibilities;
            _checkers = checkers;
            _cases = cases;
            _actions = actions;
        }

        public void Update()
        {
            _cases.isRunning = CheckIfIsRunning();
        }

        private bool CheckIfIsRunning()
        {
            //Possibility//
            if (!_possibilities.canRun) return false;
            //Grounding//
            if (!_checkers.IsGrounded()) return false;
            //Input//
            if (!_input.InputRun) return false;

            if (!_input.InputMove) return false;
            //Crouch to Run//
            if (_cases.isCrouching)
            {
                if (_checkers.HitCelling())
                {
                    return false;
                }

                _actions.InvokeOnCrouchToRunAction();
            }

            return true;
        }
    }
}