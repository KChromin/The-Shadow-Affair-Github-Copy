using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultCalculationVertical
    {
        #region Variables

        public PlayerMovementDefaultCalculationVertical(PlayerMovementDefaultScriptableObject parameters)
        {
            _parameters = parameters;
        }

        private PlayerMovementDefaultScriptableObject _parameters;

        #endregion Variables

        #region Calculation Methodes

        public Vector3 GravityForce()
        {
            return Vector3.down * -Physics.gravity.y;
        }

        #endregion Calculation Methodes
    }
}