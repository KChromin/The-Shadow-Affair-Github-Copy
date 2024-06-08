using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedStandingRun : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedStandingRun(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.standingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.StandingStates.Run;
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
            Ctx.CurrentInfo.currentStates.standingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.StandingStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            //TestWalk//
            Ctx.Controllers.Execution.BasicMove(PlayerMovementDefaultCalculationHorizontal.MovementMode.Run);

            //Handle stairs, and obstacles//
            Ctx.Controllers.StairsHandler.StepClimb(PlayerMovementDefaultStairsHandler.StepUpMode.Run);
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Cases.isRunning) return;
            SwitchStates(Ctx.Input.EnteredMove ? Factory.StandingWalk() : Factory.StandingIdle());
        }

        protected override void InitializeSubState()
        {
        }
    }
}