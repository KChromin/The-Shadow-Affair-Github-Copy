using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedCrouchingWalk : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedCrouchingWalk(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.crouchingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.CrouchingStates.Walk;
#endif

            #endregion Update State Info
            
            #region Update Physics Parameters
            
            //Update Physics Material//
            Ctx.Managers.Components.UpdateColliderPhysicsMaterial(Ctx.Parameters.physicsParameters.materialMove);

            #endregion Update Physics Parameters
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.crouchingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.CrouchingStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            //Handle stairs, and obstacles//
            Ctx.Controllers.StairsHandler.StepClimb(PlayerMovementDefaultStairsHandler.StepUpMode.CrouchWalk);
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.EnteredMove)
            {
                SwitchStates(Factory.CrouchingIdle());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}