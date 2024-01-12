using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultExecution
    {
        #region Setup

        private MovementDefaultCalculationHorizontal _horizontalCalculation;
        private MovementDefaultCalculationVertical _verticalCalculation;
        private Rigidbody _rigidbody;

        public void Setup(MovementDefaultCalculationHorizontal calculationHorizontal, MovementDefaultCalculationVertical calculationVertical, Rigidbody rigidbody)
        {
            _horizontalCalculation = calculationHorizontal;
            _verticalCalculation = calculationVertical;
            _rigidbody = rigidbody;
        }

        #endregion Setup

        public void MoveBasic()
        {
            _rigidbody.AddRelativeForce(_horizontalCalculation.MoveGrounded(DefaultMovementMoveModes.Walk), ForceMode.VelocityChange);
        }

        public void BidaGravity()
        {
            _rigidbody.AddRelativeForce(Vector3.down * 20, ForceMode.Acceleration);
        }
    }
}