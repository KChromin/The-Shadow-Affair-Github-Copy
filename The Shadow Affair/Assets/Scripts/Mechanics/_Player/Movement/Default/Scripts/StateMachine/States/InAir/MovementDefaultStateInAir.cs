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
            Ctx.MovementBase.CurrentStatesInfo.generalState = MovementDefault.CurrentStatesInfoClass.GeneralStates.InAir;
#endif

            #endregion Set State Info

            //On Airborne event//
            Ctx.MovementBase.InvokeOnAirborneAction();
        }

        protected override void InitializeSubState()
        {
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            Ctx.Execution.MoveBasic();
            Ctx.Execution.BidaGravity();
        }

        protected override void ExitState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.MovementBase.CurrentStatesInfo.generalState = MovementDefault.CurrentStatesInfoClass.GeneralStates.Disabled;
#endif

            #endregion Set State Info
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.MovementDefaultCheckers.CheckIsGrounded())
            {
                SwitchStates(Factory.Grounded());
            }
        }
    }
}