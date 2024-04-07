using SmugRag.Templates.StateMachines;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGroundedStandingRun : MovementDefaultStateBase
    {
        public MovementDefaultStateGroundedStandingRun(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.standingState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.StandingStates.Run;
#endif

            #endregion Set State Info

            //Invoke Action//
            Ctx.Actions.InvokeOnRunAction();

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

            //Invoke Action//
            Ctx.Actions.InvokeOnRunEndAction();
        }

        protected override void UpdateState()
        {
            //Head Bobbing//
            Ctx.HeadBobbing.Execute(MovementDefaultHeadBobbingModes.Run);
        }

        protected override void FixedUpdateState()
        {
            //Apply horizontal move//
            Ctx.Execution.HorizontalMoveDefault(DefaultMovementMoveModes.Run, Ctx.Checkers.IsOnSlope(), Ctx.Checkers.GetIfSlopeIsTooSteep(), Ctx.Checkers.GetSlopeNormal());
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Input.InputMove || !Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.StandingIdle());
            }
            else if (!Ctx.Input.InputRun)
            {
                SwitchStates(Factory.StandingWalk());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}