using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedStanding : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedStanding(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.groundedState = PlayerMovementDefault.InfoClass.CurrentStatesClass.GroundedStates.Standing;
#endif

            #endregion Update State Info
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.groundedState = PlayerMovementDefault.InfoClass.CurrentStatesClass.GroundedStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
            //Check is running//
            Ctx.Controllers.Run.CheckIsRunning();
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Cases.isCrouching && Ctx.Possibilities.canCrouch)
            {
                SwitchStates(Factory.Crouching());
            }
        }

        protected override void InitializeSubState()
        {
            if (Ctx.Input.EnteredMove)
            {
                if (Ctx.Cases.isRunning)
                {
                    SetSubState(Factory.StandingRun());
                }
                else
                {
                    SetSubState(Factory.StandingWalk());
                }
            }
            else
            {
                SetSubState(Factory.StandingIdle());
            }
        }
    }
}