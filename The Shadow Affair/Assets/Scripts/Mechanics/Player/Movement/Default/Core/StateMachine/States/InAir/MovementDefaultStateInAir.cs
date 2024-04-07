using SmugRag.Templates.StateMachines;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateInAir : MovementDefaultStateBase
    {
        public MovementDefaultStateInAir(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            isRootState = true;
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.generalState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GeneralStates.InAir;
#endif

            #endregion Set State Info

            //On Airborne event//
            Ctx.Actions.InvokeOnAirborneAction();

            //Update Drag//
            Ctx.ComponentsSetter.ChangeDrag(MovementDefaultComponentsSetterData.DragModes.InAir);

            //Update Physics Material//
            Ctx.ComponentsSetter.ChangePhysicsMaterial(MovementDefaultComponentsSetterData.PhysicsMaterials.InAir);
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.generalState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GeneralStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void UpdateState()
        {
            //When on air, smoothly reset headbobing//
            Ctx.HeadBobbing.SmoothReset();
            
            //Landing shake - Here for reset//
            Ctx.HeadBobbing.UpdateLandingShake();
        }

        protected override void FixedUpdateState()
        {
            //Apply Gravity//
            Ctx.Execution.VerticalGravityDefault();

            //Apply move//
            Ctx.Execution.HorizontalMoveDefault(DefaultMovementMoveModes.InAir, Ctx.Checkers.IsOnSlope(), Ctx.Checkers.GetIfSlopeIsTooSteep(), Ctx.Checkers.GetSlopeNormal());
        }

        protected override void InitializeSubState()
        {
            SetSubState(Ctx.RigidBody.velocity.y < 0 ? Factory.InAirFalling() : Factory.InAirRising());
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Base.IsEnabled)
            {
                SwitchStates(Factory.Disabled());
            }

            if (Ctx.Checkers.IsGrounded())
            {
                SwitchStates(Factory.Grounded());
            }
        }
    }
}