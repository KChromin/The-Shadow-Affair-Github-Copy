using SmugRagGames.Player.Movement.Input;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultRun
    {
        public PlayerMovementDefaultRun(PlayerMovementInput input, PlayerMovementCases cases, PlayerMovementPossibilities possibilities)
        {
            _input = input;
            _cases = cases;
            _possibilities = possibilities;
        }

        private readonly PlayerMovementInput _input;
        private readonly PlayerMovementPossibilities _possibilities;
        private readonly PlayerMovementCases _cases;

        public void CheckIsRunning()
        {
            _cases.isRunning = IsRunning();
        }

        private bool IsRunning()
        {
            if(!_input.EnteredRun) return false;
            return _possibilities.canRun;
        }
    }
}