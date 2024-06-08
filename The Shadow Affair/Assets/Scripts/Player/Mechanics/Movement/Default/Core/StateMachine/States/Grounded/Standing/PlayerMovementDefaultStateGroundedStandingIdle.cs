using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedStandingIdle : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedStandingIdle(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.standingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.StandingStates.Idle;
#endif

            #endregion Update State Info
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
            #region Update Physics Parameters
            
            //Update Physics Material//
            Ctx.Managers.MovementDefaultComponents.IdlePhysicsMaterialUpdater();

            #endregion Update Physics Parameters
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.EnteredMove) return;
            if (Ctx.Cases.isRunning)
            {
                SwitchStates(Factory.StandingRun());
            }
            else
            {
                SwitchStates(Factory.StandingWalk());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}