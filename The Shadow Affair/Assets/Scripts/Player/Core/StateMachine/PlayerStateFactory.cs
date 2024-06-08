using System.Collections.Generic;
using SmugRagGames.Patterns.StateMachine;

/*      How to Add State?
0. Create new state script
1. Create Enum with al states
2. Add a new state to enum
3. Assign it to directory
4. Create new State return
*/

namespace SmugRagGames.Player.StateMachine
{
    public class PlayerStateFactory : StateFactory
    {
        private readonly Dictionary<PlayerStates, StateBase> _states = new();

        public PlayerStateFactory(StateContext context)
        {
            _states[PlayerStates.Disabled] = new PlayerStateDisabled(context, this);
            _states[PlayerStates.Default] = new PlayerStateDefault(context, this);
            _states[PlayerStates.Dead] = new PlayerStateDead(context, this);
        }

        #region State get methodes

        public StateBase Disabled()
        {
            return _states[PlayerStates.Disabled];
        }

        public StateBase Default()
        {
            return _states[PlayerStates.Default];
        }

        public StateBase Dead()
        {
            return _states[PlayerStates.Dead];
        }

        #endregion State get methodes
    }

    public enum PlayerStates
    {
        Disabled, //Player functionalities are suspended//
        Default,
        Dead
    }
}