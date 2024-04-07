using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCalculationVertical
    {
        private MovementDefaultJumpData _jumpData;
        private MovementDefaultFactorsData _factors;

        private const float AntiDragConstantInAir = 1.02f;

        public void Setup(MovementDefaultJumpData jumpData, MovementDefaultFactorsData factors)
        {
            _jumpData = jumpData;
            _factors = factors;
        }

        public static Vector3 GravityForce()
        {
            return Physics.gravity * AntiDragConstantInAir;
        }

        public Vector3 GravityForceGrounded()
        {
            return Physics.gravity * _factors.groundingForceMultiplier;
        }

        public Vector3 GroundingForceSlope(Vector3 currentSlopeNormal)
        {
            return -currentSlopeNormal * _factors.groundingForceSlopeMultiplier;
        }

        public Vector3 JumpForce(float currentMass)
        {
            return _jumpData.jumpForce * currentMass * AntiDragConstantInAir * Vector3.up;
        }
    }
}