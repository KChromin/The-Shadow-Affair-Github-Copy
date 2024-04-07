using System;
using SmugRag.Managers.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefault : MonoBehaviour
    {
        #region Current info

#if UNITY_EDITOR

        [field: Header("Info")]
        [field: SerializeField]
        public MovementInfoClass MovementInfo { get; set; }

        [Serializable]
        public class MovementInfoClass
        {
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
            public MovementDefaultModifiersData currentModifiersDataInfo;

            #endregion Current Modifiers Info

            #region Current possibilities info

            [field: Header("Current Possibilities Info")]
            [field: SerializeField]
            public MovementDefaultPossibilitiesData CurrentPossibilitiesDataInfo { get; set; }

            #endregion Current possibilities info

            #region Current Cases Info

            [field: Header("Current Cases Info")]
            [field: SerializeField]
            public MovementDefaultCasesData CurrentCasesDataInfo { get; set; }

            #endregion Current Cases Info
        }
#endif

        #endregion Current Info

        #region Is Enabled

        [field: Header("Enabled")]
        [field: SerializeField]
        public bool IsEnabled { get; private set; } = true;

        #endregion IsEnabled

        #region Parameters

        [Header("Movement Parameters")]
        [SerializeField]
        private MovementDefaultScriptableObject movementDefaultData;

        #endregion Parameters

        #region Main components

        [Space, Header("Components")]
        [SerializeField]
        private Rigidbody rigidBody;
        [SerializeField]
        private CapsuleCollider movementCollider;

        [Header("Transforms")]
        [SerializeField]
        private Transform headOrigin;
        [SerializeField]
        private Transform headBase;
        [SerializeField]
        private Transform headBaseHeadBobbing;
        [SerializeField]
        private Transform footstepsOrigin;


        [Space, SerializeField]
        private Transform orientation;
        [SerializeField]
        private Transform checkersOrigin;

        #endregion Main components

        #region Private classes

        //Input//
        private MovementDefaultInput _input;

        //Checkers//
        private MovementDefaultCheckers _checkers;

        //Components setter//
        private MovementDefaultComponentsSetter _componentSetter;

        //Height//
        private MovementDefaultCrouchHeight _crouchHeight;

        //Cases//
        private MovementDefaultCasesData _cases;

        //Action Values Updater//
        private MovementDefaultActionValueUpdater _actionValueUpdater;

        //Modules//
        private MovementDefaultCrouch _crouch;
        private MovementDefaultRun _run;
        private MovementDefaultJump _jump;
        private MovementDefaultHeadBobbing _headBobbing;
        private MovementDefaultFootsteps _footsteps;

        //State machine//
        private MovementDefaultContext _contextData;
        private MovementDefaultStateFactory _stateFactory;

        //Movement calculation & execution//
        private MovementDefaultCalculationHorizontal _calculationHorizontal;
        private MovementDefaultCalculationVertical _calculationVertical;
        private MovementDefaultExecution _execution;

        #endregion Private classes

        #region Public classes

        //Possibilities//
        public MovementDefaultPossibilities Possibilities { get; private set; }

        //Modifiers//
        public MovementDefaultModifiers Modifiers { get; private set; }

        //Events//
        public MovementDefaultActions Actions { get; private set; }

        #endregion Public classes

        #region Setups

        private void Awake()
        {
            //Setup classes//
            Setup();

            //State Machine//
            SetupStateMachine();
        }

        private void Setup()
        {
            #region Input

            //Input//
            _input = new MovementDefaultInput();
            _input.Setup(InputManager.Instance.CurrentInput);

            #endregion Input

            #region Modifiers

            //Modifiers//
            Modifiers = new MovementDefaultModifiers();

            #endregion Modifiers

            #region Possibilities

            //Possibilities//
            Possibilities = new MovementDefaultPossibilities();
            Possibilities.Setup();

#if UNITY_EDITOR
            MovementInfo.CurrentPossibilitiesDataInfo = Possibilities.CurrentData;
#endif

            #endregion Possibilities

            #region Cases

            _cases = new MovementDefaultCasesData();

#if UNITY_EDITOR

            MovementInfo.CurrentCasesDataInfo = _cases;

#endif

            #endregion Cases

            #region Checkers

            //Checkers//
            _checkers = new MovementDefaultCheckers();
            _checkers.Setup(movementDefaultData, checkersOrigin, movementCollider);

            #endregion Checkers

            #region Actions

            //Actions//
            Actions = new MovementDefaultActions();

            #endregion Actions

            #region Action Value Updater

            _actionValueUpdater = new MovementDefaultActionValueUpdater();
            _actionValueUpdater.Setup(rigidBody, Actions);

            #endregion Action Value Updater

            #region Modules

            //Cases//
            //Initialize//
            _run = new MovementDefaultRun();
            _crouch = new MovementDefaultCrouch();
            _headBobbing = new MovementDefaultHeadBobbing();
            _footsteps = new MovementDefaultFootsteps();

            //Setup//
            _run.Setup(_input, Possibilities.CurrentData, _checkers, _cases, Actions);
            _crouch.Setup(_input, Possibilities.CurrentData, _cases, Actions, _checkers);
            _headBobbing.Setup(headBase, headBaseHeadBobbing, movementDefaultData.headBobbingData, Actions);
            _footsteps.Setup(Actions, movementDefaultData.footstepsData, footstepsOrigin, _cases, movementDefaultData.checkerParametersData.groundLayers);

            #endregion Modules

            #region Component Setter

            //Component Setters//
            _componentSetter = new MovementDefaultComponentsSetter();
            _componentSetter.Setup(rigidBody, movementCollider, movementDefaultData.componentsSetterData);

            #endregion Height

            #region Height

            //Height//
            _crouchHeight = new MovementDefaultCrouchHeight();
            _crouchHeight.Setup(movementDefaultData.crouchHeightData, headOrigin, movementCollider, Actions, _input);

            #endregion Height

            #region Execution

            //Execution//
            //Instantiate//
            _calculationHorizontal = new MovementDefaultCalculationHorizontal();
            _calculationVertical = new MovementDefaultCalculationVertical();
            _execution = new MovementDefaultExecution();

            //Setup//
            _calculationHorizontal.Setup(_input, orientation, movementDefaultData.speedData, movementDefaultData.factorsData, _actionValueUpdater, Modifiers.CurrentData);
            _calculationVertical.Setup(movementDefaultData.jumpData, movementDefaultData.factorsData);
            _execution.Setup(_calculationHorizontal, _calculationVertical, Actions, rigidBody);

            #endregion Execution

            #region Jump

            //Jump//
            _jump = new MovementDefaultJump();

            //Setup//
            _jump.Setup(_input, _checkers, Possibilities.CurrentData, _componentSetter, movementDefaultData.jumpData, _cases, Actions, _execution);

            #endregion Jump
        }

        private void SetupStateMachine()
        {
            //Setup Conext//
            _contextData = new MovementDefaultContext();
            _contextData.Setup(this, rigidBody, Actions, Possibilities.CurrentData, _input, _cases, _checkers, Modifiers.CurrentData, _headBobbing, _execution, _componentSetter);

            //Create new state factory//
            _stateFactory = new MovementDefaultStateFactory(_contextData);

            //Set first state//
            _contextData.CurrentState = _stateFactory.InAir();
            _contextData.CurrentState.EnterStates();
        }

        #endregion Setups

        #region Enabling

        public void ChangeEnableState(bool newState)
        {
            IsEnabled = newState;
        }

        #endregion Enabling

        private void Update()
        {
            _input.UpdateInputStates();
            
            #region Update Infos

#if UNITY_EDITOR
            MovementInfo.currentModifiersDataInfo = Modifiers.CurrentData;
#endif

            #endregion Update Infos

            #region Update Cases

            _run.Update();

            #endregion Update Cases

            _contextData.CurrentState.UpdateStates();

            _crouchHeight.Update();

            //Steep Slopes//
            if (_checkers.IsOnSlope() && _checkers.GetIfSlopeIsTooSteep())
            {
                _execution.HorizontalSteepSlopeSlide(_checkers.GetSlopeNormal());
            }
        }

        private void FixedUpdate()
        {
            _contextData.CurrentState.FixedUpdateStates();
        }

        private void OnDisable()
        {
            //Unsubscribe from events//
            _crouch.UnsubscribeFromEvents();
            _crouchHeight.UnsubscribeFromEvents();
            _jump.UnsubscribeFromEvents();
            _execution.UnsubscribeFromEvents();
            _actionValueUpdater.UnsubscribeFromEvents();
            _headBobbing.UnsubscribeFromEvents();
            _footsteps.UnsubscribeFromEvents();
        }

        #region Debug Gizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            _checkers.DebugDrawGizmos();
        }
#endif

        #endregion Debug Gizmos
    }
}