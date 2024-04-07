using System.Collections;
using System.Collections.Generic;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedCrouchingIdle : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedCrouchingIdle(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.crouchingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.CrouchingStates.Idle;
#endif

            #endregion Set State Info
            
            //Update Physics Material//
            Ctx.ComponentsSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.Idle);
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.crouchingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.CrouchingStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void UpdateState()
        {
            //Head Bobbing//
            Ctx.HeadBobbing.Execute(MovementDefaultHeadBobbingModes.CrouchIdle);
        }

        protected override void FixedUpdateState()
        {

        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Input.InputMove && Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.CrouchingWalk());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}