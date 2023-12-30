namespace SmugRag.Templates.StateMachines
{
    public abstract class HierarchicalStateBase : StateBase
    {
        protected HierarchicalStateBase(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        //Is a state a root//
        protected bool isRootState { get; set; }

        //Sub and super states//
        private HierarchicalStateBase currentSubState { get; set; }
        private HierarchicalStateBase currentSuperState { get; set; }

        //Initialize Sub-States//
        protected abstract void InitializeSubState();

        //Enter State, and Initialize SubState//
        public override void EnterStates()
        {
            base.EnterStates();

            InitializeSubState();
        }

        //Updates main and sub states//
        public override void UpdateStates()
        {
            base.UpdateStates();

            //Update All Sub States//
            currentSubState?.UpdateStates();
        }

        //Like Update but in fixed update//
        public override void FixedUpdateStates()
        {
            base.FixedUpdateStates();

            //Update All Sub States//
            currentSubState?.FixedUpdateStates();
        }

        //Switches main and sub states//
        protected override void CheckSwitchStates()
        {
            base.CheckSwitchStates();

            currentSubState?.CheckSwitchStates();
        }

        //Exits main and sub states//
        protected override void ExitStates()
        {
            base.ExitStates();

            //Exit State in sub-states//
            currentSubState?.ExitStates();
        }

        //Switch state//
        protected void SwitchStates(HierarchicalStateBase newState)
        {
            //Exit actual state//
            ExitStates();

            if (isRootState)
            {
                //Set new root//
                context.currentState = newState;
                //Enter new state//
                context.currentState.EnterStates();
            }
            else
            {
                currentSuperState?.SetSubState(newState);
            }
        }

        //Set super state//
        protected void SetSuperState(HierarchicalStateBase newSuperState)
        {
            currentSuperState = newSuperState;
        }

        //Set sub state//
        protected void SetSubState(HierarchicalStateBase newSubState)
        {
            currentSubState = newSubState;
            currentSubState.SetSuperState(this);
            currentSubState.EnterStates();
        }
    }
}