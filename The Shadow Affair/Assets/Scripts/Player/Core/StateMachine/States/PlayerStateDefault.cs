using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.StateMachine
{
    public class PlayerStateDefault : PlayerStateBase
    {
        public PlayerStateDefault(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.PlayerBase.CurrentInfo.CurrentState = PlayerStates.Default;
#endif

            #endregion Update State Info

            ChangeMechanicsEnabledness(true);
        }

        protected override void ExitState()
        {
            ChangeMechanicsEnabledness(false);
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

        private void ChangeMechanicsEnabledness(bool newState)
        {
            Ctx.RotationDefault.ChangeEnabledness(newState);
            Ctx.MovementDefault.ChangeEnabledness(newState);
        }
    }
}