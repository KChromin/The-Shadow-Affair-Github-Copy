using System;
using UnityEngine;
using SmugRagGames.Player.Movement.Input;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultCalculationHorizontal
    {
        #region Variables

        public PlayerMovementDefaultCalculationHorizontal(PlayerMovementDefaultScriptableObject parameters, PlayerMovementInput input, PlayerMovementDefaultCheckers checkers, PlayerMovementModifiers.ModifiersDataClass modifiers, Transform orientation)
        {
            _parameters = parameters;
            _input = input;
            _checkers = checkers;
            _modifiers = modifiers;
            _orientation = orientation;
        }

        private readonly PlayerMovementInput _input;
        private readonly PlayerMovementDefaultScriptableObject _parameters;

        private readonly PlayerMovementDefaultCheckers _checkers;
        private readonly PlayerMovementModifiers.ModifiersDataClass _modifiers;

        private readonly Transform _orientation;

        private const float SteepSlopeVectorReductionFactor = 0.1f;

        private float _slopeProjectionUpwardCompensationValue;

        public enum MovementMode
        {
            Walk,
            Run,
            CrouchWalk,
            InAir
        }

        #endregion Variables

        #region Private methodes

        //Direction//
        private Vector3 GetCurrentDirection()
        {
            //Get Input//
            Vector2 input = _input.GetValueMove();

            #region Factors

            //Apply factors//

            //Sideways//
            input.x *= _parameters.movementFactors.sidewaysMovement;

            //Backwards//
            if (input.y < 0)
            {
                input.y *= _parameters.movementFactors.backwardsMovement;
            }

            #endregion Factors

            //Convert Input to 3D//
            Vector3 input3D = new(input.x, 0, input.y);

            //Convert to current Direction//
            return _orientation.localRotation * input3D;
        }

        //Slope Handling//

        #region Slope

        private Vector3 DirectionAfterSlopeHandle()
        {
            Vector3 currentDirection = GetCurrentDirection();

            //Reset current Compensation//
            _slopeProjectionUpwardCompensationValue = 0;

            if (!_checkers.IsOnSlope()) return currentDirection;
            Vector3 directionAfterProjection = Vector3.ProjectOnPlane(currentDirection, _checkers.GetSlopeNormal());

            //Handling steep slopes//
            if (_checkers.GetIsSlopeTooSteep())
            {
                //Reduce speed only while going up//
                if (directionAfterProjection.y > 0)
                {
                    directionAfterProjection *= SteepSlopeVectorReductionFactor;
                }
            }
            else
            {
                //When walking up, reduce up value for fixing jumping off ledge//
                if (directionAfterProjection.y > 0)
                {
                    _slopeProjectionUpwardCompensationValue = directionAfterProjection.y * 2;
                    directionAfterProjection.y = 0f;
                }
            }

            return directionAfterProjection;
        }

        private Vector3 DirectionAfterSlopeAndGroundingForce()
        {
            Vector3 projectedDirection = DirectionAfterSlopeHandle();

            projectedDirection += -_checkers.GetSlopeNormal() * _parameters.physicsParameters.additionalGroundingForce;

            return projectedDirection;
        }

        #endregion Slope

        //Calculate Speed//
        private float CurrentSpeedForce(float maxSpeed)
        {
            return (maxSpeed + _slopeProjectionUpwardCompensationValue) * _parameters.physicsParameters.AntiDragValueForDrag10 * _modifiers.moveSpeedMultiplier;
        }

        private Vector3 GetCalculatedMoveVectorGround(float maxSpeed)
        {
            return DirectionAfterSlopeAndGroundingForce() * CurrentSpeedForce(maxSpeed);
        }

        private Vector3 GetCalculatedMoveVectorInAir(float maxSpeed)
        {
            return Vector3.zero * CurrentSpeedForce(maxSpeed);
        }

        #endregion Private methodes

        #region Public methodes

        public Vector3 MoveVector(MovementMode movementMode)
        {
            return movementMode switch
            {
                MovementMode.Walk => GetCalculatedMoveVectorGround(_parameters.movementSpeeds.maxSpeedWalk),
                MovementMode.Run => GetCalculatedMoveVectorGround(_parameters.movementSpeeds.maxSpeedRun),
                MovementMode.CrouchWalk => GetCalculatedMoveVectorGround(_parameters.movementSpeeds.maxSpeedCrouchWalk),
                MovementMode.InAir => GetCalculatedMoveVectorInAir(_parameters.movementSpeeds.maxSpeedInAir),
                _ => throw new ArgumentOutOfRangeException(nameof(movementMode), movementMode, null)
            };
        }

        public Vector3 DirectionForSteepSlopeSliding()
        {
            Vector3 slopeNormal = _checkers.GetSlopeNormal();
            return (Vector3.up - slopeNormal * Vector3.Dot(Vector3.up, slopeNormal)) * (Physics.gravity.y * _parameters.physicsParameters.steepSlopeSlidingForce);
        }

        #endregion Public methodes
    }
}