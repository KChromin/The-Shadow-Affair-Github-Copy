using System.Collections.Generic;
using SmugRag.Templates.StateMachines;

/*      How to Add State?
0. Create new state script
1. Create Enum with al states
2. Add a new state to enum
3. Assign it to directory
4. Create new State return
*/

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultStateFactory : StateFactory
    {
        private readonly Dictionary<MovementDefaultStates, HierarchicalStateBase> _states = new();

        public MovementDefaultStateFactory(StateContext context)
        {
            _states[MovementDefaultStates.Disabled] = new MovementDefaultStateDisabled(context, this);

            #region Grounded

            _states[MovementDefaultStates.Grounded] = new MovementDefaultStateGrounded(context, this);

            //Standing//
            _states[MovementDefaultStates.Standing] = new MovementDefaultStateGroundedStanding(context, this);
            _states[MovementDefaultStates.StandingIdle] = new MovementDefaultStateGroundedStandingIdle(context, this);
            _states[MovementDefaultStates.StandingWalk] = new MovementDefaultStateGroundedStandingWalk(context, this);
            _states[MovementDefaultStates.StandingRun] = new MovementDefaultStateGroundedStandingRun(context, this);

            //Crouching//
            _states[MovementDefaultStates.Crouching] = new MovementDefaultStateGroundedCrouching(context, this);
            _states[MovementDefaultStates.CrouchingIdle] = new MovementDefaultStateGroundedCrouchingIdle(context, this);
            _states[MovementDefaultStates.CrouchingWalk] = new MovementDefaultStateGroundedCrouchingWalk(context, this);

            #endregion Grounded

            #region In Air

            _states[MovementDefaultStates.InAir] = new MovementDefaultStateInAir(context, this);
            _states[MovementDefaultStates.InAirRising] = new MovementDefaultStateInAirRising(context, this);
            _states[MovementDefaultStates.InAirFalling] = new MovementDefaultStateInAirFalling(context, this);

            #endregion In Air
        }

        #region State Methodes

        public HierarchicalStateBase Disabled()
        {
            return _states[MovementDefaultStates.Disabled];
        }

        #region Grounded

        public HierarchicalStateBase Grounded()
        {
            return _states[MovementDefaultStates.Grounded];
        }

        #region Standing

        public HierarchicalStateBase Standing()
        {
            return _states[MovementDefaultStates.Standing];
        }

        public HierarchicalStateBase StandingIdle()
        {
            return _states[MovementDefaultStates.StandingIdle];
        }

        public HierarchicalStateBase StandingWalk()
        {
            return _states[MovementDefaultStates.StandingWalk];
        }

        public HierarchicalStateBase StandingRun()
        {
            return _states[MovementDefaultStates.StandingRun];
        }

        #endregion Standing

        #region Crouching

        public HierarchicalStateBase Crouching()
        {
            return _states[MovementDefaultStates.Crouching];
        }

        public HierarchicalStateBase CrouchingIdle()
        {
            return _states[MovementDefaultStates.CrouchingIdle];
        }

        public HierarchicalStateBase CrouchingWalk()
        {
            return _states[MovementDefaultStates.CrouchingWalk];
        }

        #endregion Crouching

        #endregion Grounded

        #region InAir

        public HierarchicalStateBase InAir()
        {
            return _states[MovementDefaultStates.InAir];
        }

        public HierarchicalStateBase InAirRising()
        {
            return _states[MovementDefaultStates.InAirRising];
        }

        public HierarchicalStateBase InAirFalling()
        {
            return _states[MovementDefaultStates.InAirFalling];
        }

        #endregion InAir

        #endregion State Methodes
    }

    //States//
    internal enum MovementDefaultStates
    {
        Disabled,
        Grounded,
        Standing,
        StandingIdle,
        StandingWalk,
        StandingRun,
        Crouching,
        CrouchingIdle,
        CrouchingWalk,
        InAir,
        InAirRising,
        InAirFalling
    }
}