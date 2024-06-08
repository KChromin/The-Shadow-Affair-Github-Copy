using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateDisabled : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateDisabled(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            IsRootState = true;
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.mainState = PlayerMovementDefault.InfoClass.CurrentStatesClass.MainStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void ExitState()
        {
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
        }

        protected override void CheckSwitchState()
        {
            //When mechanic is disabled, don't try to switch//
            if (!Ctx.Base.GetCurrentEnabledness()) return;

            if (Ctx.Possibilities.canMove)
            {
                SwitchStates(Ctx.Checkers.IsGrounded() ? Factory.Grounded() : Factory.InAir());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}