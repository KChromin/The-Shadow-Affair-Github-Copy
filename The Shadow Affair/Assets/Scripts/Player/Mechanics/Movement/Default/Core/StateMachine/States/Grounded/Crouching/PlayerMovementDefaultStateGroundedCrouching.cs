using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedCrouching : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedCrouching(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.groundedState = PlayerMovementDefault.InfoClass.CurrentStatesClass.GroundedStates.Crouching;
#endif

            #endregion Update State Info
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.groundedState = PlayerMovementDefault.InfoClass.CurrentStatesClass.GroundedStates.Disabled;
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
            if (!Ctx.Cases.isCrouching || !Ctx.Possibilities.canCrouch)
            {
                SwitchStates(Factory.Standing());
            }
        }

        protected override void InitializeSubState()
        {
            SetSubState(Ctx.Input.EnteredMove ? Factory.CrouchingWalk() : Factory.CrouchingIdle());
        }
    }
}