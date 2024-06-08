using SmugRagGames.Patterns.StateMachine;

namespace SmugRagGames.Player.Movement.Default
{
    public abstract class PlayerMovementDefaultStateBase : HierarchicalStateBase
    {
        protected readonly PlayerMovementDefaultStateContext Ctx;
        protected readonly PlayerMovementDefaultStateFactory Factory;

        protected PlayerMovementDefaultStateBase(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            Ctx = (PlayerMovementDefaultStateContext)currentContext;
            Factory = (PlayerMovementDefaultStateFactory)stateFactory;
        }
    }
}