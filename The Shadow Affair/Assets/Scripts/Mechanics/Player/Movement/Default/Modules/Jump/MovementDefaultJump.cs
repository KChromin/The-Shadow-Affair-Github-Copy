using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultJump
    {
        private MovementDefaultInput _input;
        private MovementDefaultCheckers _checkers;
        private MovementDefaultPossibilitiesData _possibilities;
        private MovementDefaultComponentsSetter _componentSetter;
        private MovementDefaultJumpData _jumpData;
        private MovementDefaultCasesData _cases;
        private MovementDefaultActions _actions;
        private MovementDefaultExecution _execution;

        public bool _alreadyJumped; //To prevent double jump near the ground//
        private bool _wentAirborne;
        private bool _onCooldown;

        public void Setup(MovementDefaultInput input, MovementDefaultCheckers checkers, MovementDefaultPossibilitiesData possibilities, MovementDefaultComponentsSetter setter, MovementDefaultJumpData jumpData, MovementDefaultCasesData cases, MovementDefaultActions actions, MovementDefaultExecution execution)
        {
            _input = input;
            _checkers = checkers;
            _possibilities = possibilities;
            _componentSetter = setter;
            _jumpData = jumpData;
            _cases = cases;
            _actions = actions;
            _execution = execution;

            SubscribeToEvents();
        }

        private async void ExecuteCooldown()
        {
            _onCooldown = true;
            await Task.Delay(Mathf.RoundToInt((_jumpData.jumpCooldown * 1000) * Time.timeScale));
            _onCooldown = false;
        }

        private void OnGrounding()
        {
            _alreadyJumped = false;
            _wentAirborne = false;

            if (_cases.jumped)
            {
                //Update case//
                _cases.jumped = false;

                ExecuteCooldown();
            }
        }

        private void OnAirborne()
        {
            _wentAirborne = true;
        }

        private void Jump()
        {
            if (CanCurrentlyJump())
            {
                _alreadyJumped = true;

                //Invoke action//
                _actions.InvokeOnJumpAction();

                //Set new material//
                _componentSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.InAir);

                //Execute jump//
                _execution.VerticalJump();
            }
        }

        private bool CanCurrentlyJump()
        {
            if (_cases.isCrouching)
            {
                _actions.InvokeOnFromCrouchToStandAfterJumpAction();
                return false;
            }

            if (!_possibilities.canJump) return false;

            if (_onCooldown) return false;

            if (!_checkers.IsGrounded()) return false;

            if (_checkers.GetIfSlopeIsTooSteep()) return false;

            if (_alreadyJumped && _wentAirborne) return false;

            //Update case//
            _cases.jumped = true;

            return true;
        }

        private void SubscribeToEvents()
        {
            //Assign Input//
            _input.OnInputJumpEvent += Jump;

            //Assign landing//
            _actions.OnGroundingAction += OnGrounding;
            _actions.OnAirborneAction += OnAirborne;
        }

        public void UnsubscribeFromEvents()
        {
            _input.OnInputJumpEvent -= Jump;
            _actions.OnGroundingAction -= OnGrounding;
            _actions.OnAirborneAction -= OnAirborne;
        }
    }
}