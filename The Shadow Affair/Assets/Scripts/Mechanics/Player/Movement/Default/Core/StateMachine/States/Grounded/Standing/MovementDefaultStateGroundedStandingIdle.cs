using System.Collections;
using System.Collections.Generic;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedStandingIdle : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedStandingIdle(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.standingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.StandingStates.Idle;
#endif

            #endregion Set State Info

            //Update Physics Material//
            Ctx.ComponentsSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.Idle);
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.standingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.StandingStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void UpdateState()
        {
            //Head Bobbing//
            Ctx.HeadBobbing.Execute(MovementDefaultHeadBobbingModes.Idle);
        }

        protected override void FixedUpdateState()
        {

        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.InputMove) return;
            if (!Ctx.Possibilities.canMove) return;

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