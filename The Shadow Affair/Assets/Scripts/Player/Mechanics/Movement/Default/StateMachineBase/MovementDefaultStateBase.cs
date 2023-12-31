using SmugRag.Templates.StateMachines;

namespace SmugRag.Player.Mechanics.Movement.Default
{
    //Takes care of casting to proper class, for easier use in the rest of the state machine//
    public abstract class MovementDefaultStateBase : HierarchicalStateBase
    {
        protected MovementDefaultStateBase(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
            Ctx = (MovementDefaultContext)context;
            Factory = (MovementDefaultStateFactory)stateFactory;
        }

        protected MovementDefaultContext Ctx { get; set; }
        protected MovementDefaultStateFactory Factory { get; set; }
    }
}