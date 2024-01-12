using SmugRag.Templates.Singletons;

namespace SmugRag.Managers.Input
{
    public class InputManager : SingletonPersistentManager<InputManager>
    {
        public GameControls CurrentInput { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            //Setup new input actions//
            CurrentInput = new GameControls();
        }

        #region OnEnable OnDisable

        private void OnEnable()
        {
            CurrentInput?.Enable();
        }

        private void OnDisable()
        {
            CurrentInput?.Disable();
        }

        #endregion OnEnable OnDisable
    }
}