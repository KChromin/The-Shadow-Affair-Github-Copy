using SmugRag.Templates.StateMachines;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateDisabled : MovementDefaultStateBase
    {
        public MovementDefaultStateDisabled(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            isRootState = true;
        }

        protected override void EnterState()
        {
            #region Set State Info

#if UNITY_EDITOR
            Ctx.Base.MovementInfo.CurrentStatesInfo.generalState = MovementDefault.MovementInfoClass.CurrentStatesInfoClass.GeneralStates.Disabled;
#endif

            #endregion Set State Info

            //On Airborne event//
            Ctx.Actions.InvokeOnDisableAction();
        }

        protected override void InitializeSubState()
        {
            return;
        }

        protected override void UpdateState()
        {
            return;
        }

        protected override void FixedUpdateState()
        {
            return;
        }

        protected override void ExitState()
        {
            return;
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Base.IsEnabled) return;

            SwitchStates(Ctx.Checkers.IsGrounded() ? Factory.Grounded() : Factory.InAir());
        }
    }
}