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
        private readonly Dictionary<MovementDefaultStates, StateBase> _states = new();

        public MovementDefaultStateFactory(StateContext context)
        {
            _states[MovementDefaultStates.Grounded] = new MovementDefaultStateGrounded(context, this);
            _states[MovementDefaultStates.InAir] = new MovementDefaultStateInAir(context, this);
        }

        #region State Methodes

        public StateBase Grounded()
        {
            return _states[MovementDefaultStates.Grounded];
        }

        public StateBase InAir()
        {
            return _states[MovementDefaultStates.InAir];
        }

        #endregion State Methodes
    }

    //States//
    internal enum MovementDefaultStates
    {
        Grounded,
        InAir
    }
}