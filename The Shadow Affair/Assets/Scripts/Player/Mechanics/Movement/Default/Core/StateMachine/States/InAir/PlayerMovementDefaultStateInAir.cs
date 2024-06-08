using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateInAir : PlayerMovementDefaultStateBase
    {
        public PlayerMovementDefaultStateInAir(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            IsRootState = true;
        }

        protected override void EnterState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.mainState = PlayerMovementDefault.InfoClass.CurrentStatesClass.MainStates.InAir;
#endif

            #endregion Update State Info

            #region Update Physics Parameters

            //Update Physics Material//
            Ctx.Managers.Components.UpdateColliderPhysicsMaterial(Ctx.Parameters.physicsParameters.materialInAir);

            //Update Drag//
            Ctx.Managers.Components.UpdateRigidBodyDrag(Ctx.Parameters.physicsParameters.dragInAir);
            
            #endregion Update Physics Parameters
        }

        protected override void ExitState()
        {
            #region Update State Info

#if UNITY_EDITOR
            Ctx.CurrentInfo.currentStates.mainState = PlayerMovementDefault.InfoClass.CurrentStatesClass.MainStates.Disabled;
#endif

            #endregion Update State Info
        }

        protected override void UpdateState()
        {
        }

        protected override void FixedUpdateState()
        {
            MovementExecutions();
        }

        protected override void CheckSwitchState()
        {
            if (!Ctx.Possibilities.canMove)
            {
                SwitchStates(Factory.Disabled());
            }
            else if (Ctx.Checkers.IsGrounded())
            {
                SwitchStates(Factory.Grounded());
            }
        }

        protected override void InitializeSubState()
        {
        }

        private void MovementExecutions()
        {
            //Apply Gravity//
            Ctx.Controllers.Execution.ApplyGravityDefault();
        }
    }
}