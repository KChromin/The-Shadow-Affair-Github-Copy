using SmugRag.Templates.StateMachines;

namespace SmugRag.Player.Mechanics.Movement.Default
{
    public class MovementDefaultStateInAir : MovementDefaultStateBase
    {
        public MovementDefaultStateInAir(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            isRootState = true;
        }

        protected override void EnterState()
        {
        }

        protected override void InitializeSubState()
        {
        }

        protected override void UpdateState()
        {

        }

        protected override void FixedUpdateState()
        {
        }

        protected override void ExitState()
        {
        }

        protected override void CheckSwitchState()
        {

        }
    }
}