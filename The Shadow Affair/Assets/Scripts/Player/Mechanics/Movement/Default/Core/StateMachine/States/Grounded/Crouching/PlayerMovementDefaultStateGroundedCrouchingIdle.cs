using UnityEngine;
using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateGroundedCrouchingIdle : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateGroundedCrouchingIdle(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.crouchingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.CrouchingStates.Idle;
#endif

            #endregion Update State Info
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.crouchingState = PlayerMovementDefault.InfoClass.CurrentStatesClass.CrouchingStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            #region Update Physics Parameters

            //Update Physics Material//
            Ctx.Managers.MovementDefaultComponents.IdlePhysicsMaterialUpdater();

            #endregion Update Physics Parameters
        }

        protected override void CheckSwitchState()
        {
            if (Ctx.Input.EnteredMove)
            {
                SwitchStates(Factory.CrouchingWalk());
            }
        }

        protected override void InitializeSubState()
        {
        }
    }
}