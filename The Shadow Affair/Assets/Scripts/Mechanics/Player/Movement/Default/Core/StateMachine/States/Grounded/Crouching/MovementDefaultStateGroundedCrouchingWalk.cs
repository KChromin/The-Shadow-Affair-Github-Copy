using System.Collections;
using System.Collections.Generic;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedCrouchingWalk : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedCrouchingWalk(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.crouchingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.CrouchingStates.Walk;
#endif

            #endregion Set State Info

            //Update Physics Material//
            Ctx.ComponentsSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.Move);
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
            Ctx.HeadBobbing.Execute(MovementDefaultHeadBobbingModes.CrouchWalk);
        }

        protected override void FixedUpdateState()
        {
            //Apply horizontal move//
            Ctx.Execution.HorizontalMoveDefault(DefaultMovementMoveModes.CrouchWalk, Ctx.Checkers.IsOnSlope(), Ctx.Checkers.GetIfSlopeIsTooSteep(), Ctx.Checkers.GetSlopeNormal());
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.InputMove || !Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.CrouchingIdle());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}