using SmugRagGames.Patterns.StateMachine;
using SmugRagGames.Player.Rotation;
using SmugRagGames.Player.Movement.Default;

namespace SmugRagGames.Player.StateMachine
{
    public class PlayerStateContext : StateContext
    {
        public PlayerStateContext(PlayerController playerBase, PlayerRotationDefault rotationDefault, PlayerMovementDefault movementDefault)
        {
            PlayerBase = playerBase;

            //Rotation//
            RotationDefault = rotationDefault;

            //Movement//
            MovementDefault = movementDefault;
        }

        //Player Controller//
        public PlayerController PlayerBase;

        #region Mechanics

        //Rotation//
        public PlayerRotationDefault RotationDefault;

        //Movement//
        public PlayerMovementDefault MovementDefault;

        #endregion Mechanics
    }
}