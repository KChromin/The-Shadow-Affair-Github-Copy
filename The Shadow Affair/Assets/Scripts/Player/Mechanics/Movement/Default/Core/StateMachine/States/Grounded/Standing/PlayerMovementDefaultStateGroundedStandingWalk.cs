using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedStandingWalk : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedStandingWalk(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.standingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.StandingStates.Walk;
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

            #region Update Physics Parameters

            //Update Physics Material//
            Ctx.Managers.Components.UpdateColliderPhysicsMaterial(Ctx.Parameters.physicsParameters.materialMove);

            #endregion Update Physics Parameters
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            //TestWalk//
            Ctx.Controllers.Execution.BasicMove(PlayerMovementDefaultCalculationHorizontal.MovementMode.Walk);

            //Handle stairs, and obstacles//
            Ctx.Controllers.StairsHandler.StepClimb(PlayerMovementDefaultStairsHandler.StepUpMode.Walk);
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Input.EnteredMove)
            {
                if (Ctx.Cases.isRunning)
                {
                    SwitchStates(Factory.StandingRun());
                }
            }
            else
            {
                SwitchStates(Factory.StandingIdle());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}