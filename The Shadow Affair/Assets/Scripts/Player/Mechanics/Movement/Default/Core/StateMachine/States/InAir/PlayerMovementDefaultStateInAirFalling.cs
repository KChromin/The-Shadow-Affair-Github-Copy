using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateInAirFalling : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateInAirFalling(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.inAirState = PlayerMovementDefault.InfoClass.CurrentStatesClass.InAirStates.Falling;
#endif

            #endregion Update State Info
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.inAirState = PlayerMovementDefault.InfoClass.CurrentStatesClass.InAirStates.Disabled;
#endif

            #endregion Update State Info
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

        protected override void InitializeSubState()
        {
        }
    }
}