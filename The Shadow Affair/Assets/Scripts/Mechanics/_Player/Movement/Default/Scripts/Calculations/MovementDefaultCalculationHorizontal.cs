using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCalculationHorizontal
    {
        private GameControls _input;
        private Transform _orientation;
        private MovementDefaultFactors _factors;
        private MovementDefaultModifiers _modifiers;

        public void Setup(GameControls input, Transform orientation, MovementDefaultFactors factors, MovementDefaultModifiers modifiers)
        {
            _input = input;
            _orientation = orientation;
            _factors = factors;
            _modifiers = modifiers;
        }

        #region Methodes

        #region Direction

        private Vector3 CurrentDirection()
        {
            Vector2 input = _input.Default.Movement.ReadValue<Vector2>();

            #region Factors

            //Walking sideways//
            input.x *= _factors.sidewaysMovement;

            //Walking Backwards//
            if (input.y < 0)
            {
                input.y *= _factors.backwardsMovement;
            }

            #endregion Factors

            Vector3 input3D = new(input.x, 0, input.y);

            return (_orientation.localRotation * input3D).normalized;
        }

        private Vector3 DirectionOnSlope()
        {
            Vector3 currentDirection = CurrentDirection();

            //Operations on slope//

            return currentDirection;
        }

        #endregion Direction

        #endregion Methodes

        public Vector3 MoveGrounded(DefaultMovementMoveModes moveMode, bool applyFactors = true, bool applyModifiers = true)
        {
            switch (moveMode)
            {
                case DefaultMovementMoveModes.Walk:
                    return DirectionOnSlope();
                    break;
                case DefaultMovementMoveModes.Run:
                    return DirectionOnSlope();
                    break;
                case DefaultMovementMoveModes.CrouchWalk:
                    return DirectionOnSlope();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveMode), moveMode, null);
            }
        }
    }
    
    public enum DefaultMovementMoveModes
    {
        Walk,
        Run,
        CrouchWalk
    }
}