using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.StateMachine
{
    public class PlayerStateDead : PlayerStateBase
    {
        public PlayerStateDead(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info 

#if UNITY_EDITOR
            Ctx.PlayerBase.CurrentInfo.CurrentState = PlayerStates.Dead;
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