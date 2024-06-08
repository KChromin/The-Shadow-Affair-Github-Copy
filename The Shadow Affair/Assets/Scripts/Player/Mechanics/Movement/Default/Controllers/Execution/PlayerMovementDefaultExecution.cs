using SmugRagGames.Player.Movement.Input;
using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultExecution
    {
        #region Variables

        public PlayerMovementDefaultExecution(Rigidbody rigidBody, PlayerMovementDefaultScriptableObject parameters, PlayerMovementInput input, PlayerMovementDefaultCheckers checkers, PlayerMovementModifiers.ModifiersDataClass modifiers,Transform orientation)
        {
            _rigidBody = rigidBody;
            _horizontalCalculation = new PlayerMovementDefaultCalculationHorizontal(parameters, input, checkers, modifiers,orientation);
            _verticalCalculation = new PlayerMovementDefaultCalculationVertical(parameters);
        }

        private readonly Rigidbody _rigidBody;
        private readonly PlayerMovementDefaultCalculationHorizontal _horizontalCalculation;
        private readonly PlayerMovementDefaultCalculationVertical _verticalCalculation;

        #endregion Variables

        #region Vertical Methodes

        public void ApplyGravityDefault()
        {
            _rigidBody.AddRelativeForce(_verticalCalculation.GravityForce(), ForceMode.Acceleration);
        }

        #endregion Vertical Methodes

        #region Horizontal Methodes

        public void BasicMove(PlayerMovementDefaultCalculationHorizontal.MovementMode movementMode)
        {
            _rigidBody.AddForce(_horizontalCalculation.MoveVector(movementMode), ForceMode.Acceleration);
        }

        public void SteepSlopeSliding()
        {
            _rigidBody.AddForce(_horizontalCalculation.DirectionForSteepSlopeSliding(), ForceMode.Acceleration);
        }

        public void StepClimbing(Vector3 upStepVector)
        {
            _rigidBody.AddForce(upStepVector, ForceMode.Acceleration);
        }
        #endregion Horizontal Methodes
    }
}