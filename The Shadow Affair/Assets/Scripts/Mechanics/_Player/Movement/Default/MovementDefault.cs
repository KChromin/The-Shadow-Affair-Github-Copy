using System;
using SmugRag.Managers.Input;
using SmugRag.Templates.StateMachines;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefault : MonoBehaviour
    {
        #region Current Info

#if UNITY_EDITOR

        #region Current State Info

        [field: Header("Current States")]
        [field: SerializeField]
        public CurrentStatesInfoClass CurrentStatesInfo { get; set; }

        [Serializable]
        public class CurrentStatesInfoClass
        {
            [Header("General State")]
            public GeneralStates generalState;

            public enum GeneralStates
            {
                Disabled,
                Grounded,
                InAir
            }

            #region Grounded

            [Space, Header("Grounded State")]
            public GroundedStates groundedState;

            public enum GroundedStates
            {
                Disabled,
                Standing,
                Crouching
            }

            #region Standing

            [Header("Standing State")]
            public StandingStates standingState;

            public enum StandingStates
            {
                Disabled,
                Idle,
                Walk,
                Run
            }

            #endregion Standing

            #region Crouching

            [Header("Crouching State")]
            public CrouchingStates crouchingState;

            public enum CrouchingStates
            {
                Disabled,
                Idle,
                Walk
            }

            #endregion Crouching

            #endregion Grounded

            #region In Air

            [Space, Header("In Air State")]
            public InAirStates inAirState;

            public enum InAirStates
            {
                Disabled,
                Falling,
                Rising
            }

            #endregion In Air
        }

        #endregion Current State Info

        #region Current Modifiers Info

        [field: Header("Current Modifiers Info")]
        [field: SerializeField]
        public MovementDefaultModifiers CurrentModifiersInfo { get; set; }

        #endregion Current Modifiers Info

#endif

        #endregion Current Info

        #region Parameters

        [Space, Header("Movement Parameters")]
        [SerializeField]
        private MovementDefaultScriptableObject movementDefaultData;

        #endregion Parameters
  
        #region Main Objects

        [Space, Header("Main Objects")]
        [SerializeField]
        private Rigidbody movementRigidbody;
        [Space, SerializeField]
        private Transform movementOrientation;
        [SerializeField]
        private Transform checkersOrigin;

        #endregion Main Objects

        #region Action Events

        public event Action OnGroundingAction;
        public event Action OnAirborneAction;

        #endregion Action Events

        #region Private classes

        //Input//
        private GameControls _input;

        //Checkers//
        private MovementDefaultCheckers _movementDefaultCheckers;

        //State machine//
        private MovementDefaultContext _contextData;
        private MovementDefaultStateFactory _stateFactory;

        //Movement calculation & execution//
        private MovementDefaultCalculationHorizontal _calculationHorizontal;
        private MovementDefaultCalculationVertical _calculationVertical;
        private MovementDefaultExecution _movementExecution;

        //Jump//

        //Modifiers//
        private MovementDefaultModifiersController _modifiersController;

        #endregion Private classes

        #region Setups

        private void Start()
        {
            SetupInput();
            SetupModifiers();
            SetupExecution();
            
            //State Machine//
            SetupStateMachineContext();
            SetupStateMachine();
        }

        private void SetupInput()
        {
            _input = InputManager.Instance.CurrentInput;
        }
        
        private void SetupModifiers()
        {
            //Set new modifiers//
            _modifiersController = new MovementDefaultModifiersController();
            _modifiersController.Setup();

#if UNITY_EDITOR
            //Setup current info
            CurrentModifiersInfo = _modifiersController.CurrentMovementModifiers;
#endif
        }

        private void SetupExecution()
        {
            //Instantiate//
            _calculationHorizontal = new MovementDefaultCalculationHorizontal();
            _calculationVertical = new MovementDefaultCalculationVertical();
            _movementExecution = new MovementDefaultExecution();

            //Setup//
            _calculationHorizontal.Setup(_input, movementOrientation, movementDefaultData.movementFactors, _modifiersController.CurrentMovementModifiers);
            _movementExecution.Setup(_calculationHorizontal, _calculationVertical, movementRigidbody);
        }

        private void SetupStateMachineContext()
        {
            _contextData = new MovementDefaultContext();

            //Checkers//
            SetupCheckers();

            _contextData.Setup(this, _movementDefaultCheckers, _movementExecution);
        }

        private void SetupStateMachine()
        {
            //Create new state factory//
            _stateFactory = new MovementDefaultStateFactory(_contextData);

            //Set first state//
            _contextData.CurrentState = _stateFactory.Grounded();
            _contextData.CurrentState.EnterStates();
        }

        private void SetupCheckers()
        {
            _movementDefaultCheckers = new MovementDefaultCheckers();
            _movementDefaultCheckers.Setup(movementDefaultData, checkersOrigin);
        }

        #endregion Setups

        private void Update()
        {
            _contextData.CurrentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            _contextData.CurrentState.FixedUpdateStates();
        }

        #region Action Invoke Calls

        public void InvokeOnGroundingAction()
        {
            OnGroundingAction?.Invoke();
        }

        public void InvokeOnAirborneAction()
        {
            OnAirborneAction?.Invoke();
        }

        #endregion Action Invoke Calls

        #region Debug Gizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            _movementDefaultCheckers.DebugDrawGizmos();
        }
#endif

        #endregion Debug Gizmos
    }
}