namespace SmugRagGames.Patterns.StateMachine
{
    public abstract class HierarchicalStateBase : StateBase
    {
        protected HierarchicalStateBase(StateContext currentContext, StateFactory stateFactory) : base(currentContext, stateFactory)
        {
        }

        //Is a state a root//
        protected bool IsRootState { get; set; }

        //Sub and super states//
        private HierarchicalStateBase CurrentSubState { get; set; }
        private HierarchicalStateBase CurrentSuperState { get; set; }

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
            CurrentSubState?.UpdateStates();
        }

        //Like Update but in fixed update//
        public override void FixedUpdateStates()
        {
            base.FixedUpdateStates();

            //Update All Sub States//
            CurrentSubState?.FixedUpdateStates();
        }

        //Switches main and sub states//
        protected override void CheckSwitchStates()
        {
            base.CheckSwitchStates();

            CurrentSubState?.CheckSwitchStates();
        }

        //Exits main and sub states//
        protected override void ExitStates()
        {
            base.ExitStates();

            //Exit State in sub-states//
            CurrentSubState?.ExitStates();
        }

        //Switch state//
        protected void SwitchStates(HierarchicalStateBase newState)
        {
            //Exit actual state//
            ExitStates();

            if (IsRootState)
            {
                //Set new root//
                Context.CurrentState = newState;
                //Enter new state//
                Context.CurrentState.EnterStates();
            }
            else
            {
                CurrentSuperState?.SetSubState(newState);
            }
        }

        //Set super state//
        protected void SetSuperState(HierarchicalStateBase newSuperState)
        {
            CurrentSuperState = newSuperState;
        }

        //Set sub state//
        protected void SetSubState(HierarchicalStateBase newSubState)
        {
            CurrentSubState = newSubState;
            CurrentSubState.SetSuperState(this);
            CurrentSubState.EnterStates();
        }
    }
}