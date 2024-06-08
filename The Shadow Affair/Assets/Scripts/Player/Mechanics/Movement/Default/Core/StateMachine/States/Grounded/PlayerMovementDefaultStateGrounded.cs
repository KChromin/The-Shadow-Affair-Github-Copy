using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGrounded : PlayerMovementDefaultStateBase
    {
        #region State Methodes

        public PlayerMovementDefaultStateGrounded(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            IsRootState = true;
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.mainState = PlayerMovementDefault.InfoClass.CurrentStatesClass.MainStates.Grounded;
#endif

            #endregion Update State Info

            #region Update Physics Parameters

            //Update Drag//
            Ctx.Managers.Components.UpdateRigidBodyDrag(Ctx.Parameters.physicsParameters.dragGrounded);

            #endregion Update Physics Parameters
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.mainState = PlayerMovementDefault.InfoClass.CurrentStatesClass.MainStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            SteepSlopeSliding();
            MovementExecutions();
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.Disabled());
            }
            else if (!Ctx.Checkers.IsGrounded())
            {
                SwitchStates(Factory.InAir());
            }
        }

        protected override void InitializeSubState()
        {
            if (Ctx.Cases.isCrouching && Ctx.Possibilities.canCrouch)
            {
                SetSubState(Factory.Crouching());
            }
            else
            {
                SetSubState(Factory.Standing());
            }
        }

        #endregion State Methodes

        private void SteepSlopeSliding()
        {
            if (Ctx.Checkers.IsOnSlope() && Ctx.Checkers.GetIsSlopeTooSteep())
            {
                Debug.Log("SteepSlope!");
                Ctx.Controllers.Execution.SteepSlopeSliding();
            }
        }

        private void MovementExecutions()
        {
            //Apply Gravity//
            Ctx.Controllers.Execution.ApplyGravityDefault();
        }
    }
}