using System.Collections;
using System.Collections.Generic;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateInAirFalling : MovementDefaultStateBase
    {
        public MovementDefaultStateInAirFalling(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.inAirState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.InAirStates.Falling;
#endif

            #endregion Set State Info
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.inAirState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.InAirStates.Disabled;
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
            if (Ctx.RigidBody.velocity.y >= 0)
            {
                SwitchStates(Factory.InAirRising());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}
