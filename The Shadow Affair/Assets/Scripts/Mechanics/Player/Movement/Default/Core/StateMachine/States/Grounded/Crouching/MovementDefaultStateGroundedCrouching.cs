using System.Collections;
using System.Collections.Generic;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedCrouching : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedCrouching(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.groundedState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GroundedStates.Crouching;
#endif

            #endregion Set State Info

            Ctx.Actions.InvokeOnCrouchAction();
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
            if (!Ctx.Cases.isCrouching)
            {
                SwitchStates(Factory.Standing());
            }
        }

        protected override void InitializeSubState()
        {
            SetSubState(Factory.CrouchingIdle());
        }
    }
}