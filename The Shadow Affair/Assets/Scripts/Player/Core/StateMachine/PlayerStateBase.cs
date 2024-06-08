using SmugRagGames.Patterns.StateMachine;

//Recast types for cleaner inheritance//
namespace SmugRagGames.Player.StateMachine
{
    public abstract class PlayerStateBase : StateBase
    {
        protected PlayerStateContext Ctx { get; private set; }
        protected PlayerStateFactory Factory { get; private set; }

        protected PlayerStateBase(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            Ctx = (PlayerStateContext)currentContext;
            Factory = (PlayerStateFactory)stateFactory;
        }
    }
}