using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCalculationHorizontal
    {
        private Transform _orientation;
        private MovementDefaultInput _input;
        private MovementDefaultSpeedData _speedData;
        private MovementDefaultFactorsData _factorsData;
        private MovementDefaultActionValueUpdater _actionValueUpdater;
        private MovementDefaultModifiersData _modifiersData;

        // Drag - Counter Value
        // 10 - 12.5f
        // 9 - ~11f
        // 8 - 9.53f 
        // 7 - 8.14f
        // 1 - 1.02f

        private const float AntiDragConstantGround = 8.14f;
        private const float AntiDragConstantInAir = 1.02f;

        private const float SteepSlopeMoveReductionConstant = 0.08f;


        public void Setup(MovementDefaultInput input, Transform orientation, MovementDefaultSpeedData speedData, MovementDefaultFactorsData factorsData, MovementDefaultActionValueUpdater actionValueUpdater, MovementDefaultModifiersData modifiersData)
        {
            _input = input;
            _orientation = orientation;
            _speedData = speedData;
            _factorsData = factorsData;
            _actionValueUpdater = actionValueUpdater;
            _modifiersData = modifiersData;
        }

        #region Methodes

        #region Move Vector

        private Vector3 CurrentDirection()
        {
            Vector2 input = _input.GetInputMovement();

            #region Factors

            //Walking sideways//
            input.x *= _factorsData.sidewaysMovement;

            //Walking Backwards//
            if (input.y < 0)
            {
                input.y *= _factorsData.backwardsMovement;
            }

            #endregion Factors

            Vector3 input3D = new(input.x, 0, input.y);

            return (_orientation.localRotation * input3D);
        }

        private Vector3 DirectionAfterSlopeHandle(bool isOnSlope, bool slopeIsTooSteep, Vector3 slopeNormal)
        {
            Vector3 currentDirection = CurrentDirection();

            if (isOnSlope)
            {
                if (!slopeIsTooSteep)
                {
                    currentDirection = Vector3.ProjectOnPlane(currentDirection, slopeNormal);
                }
                else
                {
                    currentDirection *= SteepSlopeMoveReductionConstant;
                }
            }

            return currentDirection;
        }

        private float CurrentSpeedForce(Vector3 currentVelocity, float maxSpeed, AnimationCurve accelerationRate, bool inAir, Vector3 inAirOnAirborneVelocity, bool applyAntiDrag = true)
        {
            float currentSpeed;

            if (inAir)
            {
                Vector3 onAirborneMomentum = new(inAirOnAirborneVelocity.x, 0, inAirOnAirborneVelocity.z);
                Vector3 currentMomentum = new(currentVelocity.x, 0, currentVelocity.z);

                currentSpeed = (currentMomentum - onAirborneMomentum).magnitude;
            }
            else
            {
                currentSpeed = currentVelocity.magnitude;
            }

            float speedRatio = Mathf.Clamp((currentSpeed / maxSpeed), 0, 1);

            //Acceleration evaluation//
            float accelerationValue = accelerationRate.Evaluate(speedRatio);

            float finalFactor = accelerationValue * maxSpeed;

            if (applyAntiDrag)
            {
                if (inAir)
                {
                    finalFactor *= AntiDragConstantInAir;
                }
                else
                {
                    finalFactor *= AntiDragConstantGround;
                }
            }

            return finalFactor;
        }

        private Vector3 GetMoveVector(Vector3 currentVelocity, bool isOnSlope, bool slopeIsTooSteep, Vector3 isOnSlopeNormal, float maxSpeed, AnimationCurve accelerationRatio, Vector3 inAirOnAirborneVelocity, bool applyModifiers = true, bool inAir = false, bool applyAntiDrag = true)
        {
            return DirectionAfterSlopeHandle(isOnSlope, slopeIsTooSteep, isOnSlopeNormal) * CurrentSpeedForce(currentVelocity, maxSpeed, accelerationRatio, inAir, inAirOnAirborneVelocity, applyAntiDrag);
        }

        public static Vector3 DirectionForSteepSlopeSliding(Vector3 slopeNormal)
        {
            return AntiDragConstantInAir * Physics.gravity.y * (Vector3.up - slopeNormal * Vector3.Dot(Vector3.up, slopeNormal));
        }

        public Vector3 LandingMomentumReductionForce(Vector3 currentVelocity)
        {
            float reductionForce;

            if (currentVelocity.y > -_factorsData.velocityForMaximalReduction)
            {
                float ratio = Mathf.Abs(currentVelocity.y) / _factorsData.velocityForMaximalReduction;
                reductionForce = Mathf.Lerp(_factorsData.landingMomentumReductionMin, _factorsData.landingMomentumReductionMax, ratio);
            }
            else
            {
                reductionForce = _factorsData.landingMomentumReductionMax;
            }

            return -(currentVelocity * reductionForce);
        }

        #endregion Move Vector

        public Vector3 MoveGrounded(DefaultMovementMoveModes moveMode, Vector3 currentVelocity, bool isOnSlope, bool slopeIsTooSteep, Vector3 isOnSlopeNormal, bool applyModifiers = true)
        {
            switch (moveMode)
            {
                case DefaultMovementMoveModes.Walk:
                    return GetMoveVector(currentVelocity, isOnSlope, slopeIsTooSteep, isOnSlopeNormal, _speedData.maxSpeedWalk, _speedData.accelerationWalk, Vector3.zero, applyModifiers);

                case DefaultMovementMoveModes.Run:
                    return GetMoveVector(currentVelocity, isOnSlope, slopeIsTooSteep, isOnSlopeNormal, _speedData.maxSpeedRun, _speedData.accelerationRun, Vector3.zero, applyModifiers);

                case DefaultMovementMoveModes.CrouchWalk:
                    return GetMoveVector(currentVelocity, isOnSlope, slopeIsTooSteep, isOnSlopeNormal, _speedData.maxSpeedCrouch, _speedData.accelerationCrouch, Vector3.zero, applyModifiers);

                case DefaultMovementMoveModes.InAir:
                    return GetMoveVector(currentVelocity, isOnSlope, slopeIsTooSteep, isOnSlopeNormal, _speedData.maxSpeedInAir, _speedData.accelerationInAir, _actionValueUpdater.OnAirborneMomentum, applyModifiers, true, false);

                default:
                    throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null);
            }
        }

        #endregion Methodes
    }

    public enum DefaultMovementMoveModes
    {
        Walk,
        Run,
        CrouchWalk,
        InAir
    }
}