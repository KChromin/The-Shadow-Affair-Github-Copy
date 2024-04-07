namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultPossibilities
    {
        public MovementDefaultPossibilitiesData CurrentData { get; private set; }

        public void Setup()
        {
            CurrentData = new MovementDefaultPossibilitiesData();
        }

        #region Public methodes

        public void ChangePossibilityMove(bool newState)
        {
            CurrentData.canMove = newState;
        }

        public void ChangePossibilityRun(bool newState)
        {
            CurrentData.canRun = newState;
        }

        public void ChangePossibilityJump(bool newState)
        {
            CurrentData.canJump = newState;
        }

        public void ChangePossibilityCrouch(bool newState)
        {
            CurrentData.canCrouch = newState;
        }

        #endregion Public methodes
    }
}