using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedStanding : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedStanding(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.groundedState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GroundedStates.Standing;
#endif

            #endregion Set State Info

            Ctx.Actions.InvokeOnStandUpAction();
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.groundedState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GroundedStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Cases.isCrouching)
            {
                SwitchStates(Factory.Crouching());
            }
        }

        protected override void InitializeSubState()
        {
            if (!Ctx.Input.InputMove)
            {
                SetSubState(Factory.StandingIdle());
            }
            else if (Ctx.Cases.isRunning)
            {
                SetSubState(Factory.StandingRun());
            }
            else
            {
                SetSubState(Factory.StandingWalk());
            }
        }
    }
}