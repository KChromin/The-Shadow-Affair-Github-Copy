using UnityEngine;
using System.Collections.Generic;
using SmugRagGames.Patterns.StateMachine;

/*      How to Add State?
0. Create new state script
1. Add a new state to enum
2. Instance new state script and assign it to Dictionary it in Class Constructor
3. Create new State return methode
*/

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateFactory : StateFactory
    {
        private readonly Dictionary<MovementDefaultStates, HierarchicalStateBase> _states = new();

        public PlayerMovementDefaultStateFactory(StateContext context)
        {
            _states[MovementDefaultStates.Disabled] = new PlayerMovementDefaultStateDisabled(context, this);

            #region Grounded

            _states[MovementDefaultStates.Grounded] = new PlayerMovementDefaultStateGrounded(context, this);

            #region Standing

            _states[MovementDefaultStates.Standing] = new PlayerMovementDefaultStateGroundedStanding(context, this);
            _states[MovementDefaultStates.StandingIdle] = new PlayerMovementDefaultStateGroundedStandingIdle(context, this);
            _states[MovementDefaultStates.StandingWalk] = new PlayerMovementDefaultStateGroundedStandingWalk(context, this);
            _states[MovementDefaultStates.StandingRun] = new PlayerMovementDefaultStateGroundedStandingRun(context, this);

            #endregion Standings

            #region Crouching

            _states[MovementDefaultStates.Crouching] = new PlayerMovementDefaultStateGroundedCrouching(context, this);
            _states[MovementDefaultStates.CrouchingIdle] = new PlayerMovementDefaultStateGroundedCrouchingIdle(context, this);
            _states[MovementDefaultStates.CrouchingWalk] = new PlayerMovementDefaultStateGroundedCrouchingWalk(context, this);

            #endregion Crouching

            #endregion Grounded

            #region In Air

            _states[MovementDefaultStates.InAir] = new PlayerMovementDefaultStateInAir(context, this);
            _states[MovementDefaultStates.InAirFalling] = new PlayerMovementDefaultStateInAirFalling(context, this);
            _states[MovementDefaultStates.InAirRising] = new PlayerMovementDefaultStateInAirRising(context, this);

            #endregion In Air
        }

        #region State getting methodes

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

        #region In Air

        public HierarchicalStateBase InAir()
        {
            return _states[MovementDefaultStates.InAir];
        }

        public HierarchicalStateBase InAirFalling()
        {
            return _states[MovementDefaultStates.InAirFalling];
        }

        public HierarchicalStateBase InAirRising()
        {
            return _states[MovementDefaultStates.InAirRising];
        }

        #endregion In Air

        #endregion State getting methodes

        #region States enum

        //All state names//
        private enum MovementDefaultStates
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

        #endregion States enum
    }
}