using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateGrounded : MovementDefaultStateBase
    {
        public MovementDefaultStateGrounded(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            isRootState = true;
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.generalState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GeneralStates.Grounded;
#endif

            #endregion Set State Info

            //Grounding event//
            Ctx.Actions.InvokeOnGroundingAction();
            Ctx.Actions.InvokeOnLandingAction(Ctx.RigidBody.velocity.y);

            //Update Drag//
            Ctx.ComponentsSetter.ChangeDrag(MovementDefaultComponentsSetterData.DragModes.Grounded);
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.generalState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GeneralStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void InitializeSubState()
        {
            SetSubState(Factory.Standing());
        }

        protected override void UpdateState()
        {
            //Landing//
            Ctx.HeadBobbing.UpdateLandingShake();
        }

        protected override void FixedUpdateState()
        {
            //Without this check, jump is messed up//
            if (!Ctx.Cases.jumped)
            {
                //Help on slopes//
                if (Ctx.Checkers.IsOnSlope() && !Ctx.Checkers.GetIfSlopeIsTooSteep())
                {
                    Ctx.Execution.VerticalGroundingForce(Ctx.Checkers.GetSlopeNormal());
                }

                //Apply basic gravity//
                Ctx.Execution.VerticalGravityGrounded();
            }
        }


        protected override void CheckSwitchState()
        {
            if (!Ctx.Base.IsEnabled)
            {
                SwitchStates(Factory.Disabled());
            }

            if (!Ctx.Checkers.IsGrounded())
            {
                SwitchStates(Factory.InAir());
            }
        }
    }
}