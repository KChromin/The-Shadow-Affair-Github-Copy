using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedStandingWalk : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedStandingWalk(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.standingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.StandingStates.Walk;
#endif

            #endregion Set State Info

            //Update Physics Material//
            Ctx.ComponentsSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.Move);
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
            Ctx.HeadBobbing.Execute(MovementDefaultHeadBobbingModes.Walk);
        }

        protected override void FixedUpdateState()
        {
            //Apply horizontal move//
            Ctx.Execution.HorizontalMoveDefault(DefaultMovementMoveModes.Walk, Ctx.Checkers.IsOnSlope(), Ctx.Checkers.GetIfSlopeIsTooSteep(), Ctx.Checkers.GetSlopeNormal());
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.InputMove || !Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.StandingIdle());
            }
            else if (Ctx.Cases.isRunning)
            {
                SwitchStates(Factory.StandingRun());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}