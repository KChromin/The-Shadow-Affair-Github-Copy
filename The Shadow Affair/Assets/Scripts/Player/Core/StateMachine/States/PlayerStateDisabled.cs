using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.StateMachine
{
    public class PlayerStateDisabled : PlayerStateBase
    {
        public PlayerStateDisabled(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.PlayerBase.CurrentInfo.CurrentState = PlayerStates.Disabled;
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
        }
    }
}