using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultExecution
    {
        #region Setup

        private MovementDefaultCalculationHorizontal _horizontalCalculation;
        private MovementDefaultCalculationVertical _verticalCalculation;
        private MovementDefaultActions _actions;
        private Rigidbody _rigidBody;

        public void Setup(MovementDefaultCalculationHorizontal calculationHorizontal, MovementDefaultCalculationVertical calculationVertical, MovementDefaultActions actions, Rigidbody rigidBody)
        {
            _horizontalCalculation = calculationHorizontal;
            _verticalCalculation = calculationVertical;
            _actions = actions;
            _rigidBody = rigidBody;

            SubscribeToEvents();
        }

        #endregion Setup

        #region Horizontal

        public void HorizontalMoveDefault(DefaultMovementMoveModes moveMode, bool isOnSlope, bool slopeIsTooSteep, Vector3 slopeNormal)
        {
            _rigidBody.AddRelativeForce(_horizontalCalculation.MoveGrounded(moveMode, _rigidBody.velocity, isOnSlope, slopeIsTooSteep, slopeNormal), ForceMode.Acceleration);
        }

        public void HorizontalSteepSlopeSlide(Vector3 slopeNormal)
        {
            _rigidBody.AddRelativeForce(MovementDefaultCalculationHorizontal.DirectionForSteepSlopeSliding(slopeNormal), ForceMode.Acceleration);
        }

        private void HorizontalOnLandingMomentumReduction()
        {
            _rigidBody.AddRelativeForce(_horizontalCalculation.LandingMomentumReductionForce(_rigidBody.velocity), ForceMode.VelocityChange);
        }

        #endregion Horizontal

        #region Vertical

        public void VerticalGravityDefault()
        {
            _rigidBody.AddRelativeForce(MovementDefaultCalculationVertical.GravityForce(), ForceMode.Acceleration);
        }

        public void VerticalGravityGrounded()
        {
            _rigidBody.AddRelativeForce(_verticalCalculation.GravityForceGrounded(), ForceMode.Acceleration);
        }

        public void VerticalGroundingForce(Vector3 currentSlopeNormal)
        {
            _rigidBody.AddRelativeForce(_verticalCalculation.GroundingForceSlope(currentSlopeNormal), ForceMode.Acceleration);
        }

        public void VerticalJump()
        {
            _rigidBody.AddRelativeForce(_verticalCalculation.JumpForce(_rigidBody.mass), ForceMode.Impulse);
        }

        #endregion Vertical

        #region Events

        private void SubscribeToEvents()
        {
            _actions.OnGroundingAction += HorizontalOnLandingMomentumReduction;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnGroundingAction -= HorizontalOnLandingMomentumReduction;
        }

        #endregion Events
    }
}