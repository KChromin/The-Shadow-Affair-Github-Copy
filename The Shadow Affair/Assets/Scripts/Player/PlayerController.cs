using System;
using UnityEngine;
using SmugRagGames.Managers.Input;
using SmugRagGames.Managers.Settings;
using SmugRagGames.Player.StateMachine;
using SmugRagGames.Player.Rotation;
using SmugRagGames.Player.Movement;
using SmugRagGames.Player.Movement.Default;

namespace SmugRagGames.Player
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        #region Current Infos

#if UNITY_EDITOR
        [field: Header("Player Info")]
        [field: SerializeField]
        public PlayerInfoClass CurrentInfo { get; set; }

        [Serializable]
        public class PlayerInfoClass
        {
            #region State Machine

            [field: Header("Current State")]
            [field: SerializeField]
            public PlayerStates CurrentState { get; set; }

            #endregion State Machine

            #region Possibilities

            [Header("Possibilities")]
            public PlayerPossibilities currentPossibilities;

            #endregion Possibilities

            #region Cases

            [Header("Cases")]
            public PlayerCases currentCases;

            #endregion Cases
        }
#endif

        #endregion Current Infos

        #region State Machine

        private PlayerStateContext _stateContext;
        private PlayerStateFactory _stateFactory;

        #endregion State Machine

        #region Core

        private GameControls _input;

        private PlayerComponentsManager _componentsManager;

        private PlayerModifiers _modifiers;

        private PlayerPossibilities _possibilities;

        private PlayerCases _cases;

        private PlayerActions _actions;

        #endregion Core

        #region Mechanics

        #region Rotation

        private PlayerRotationDefault _rotationDefault;

        #endregion Rotation

        #region Movement

        private PlayerMovementDefault _movementDefault;

        #endregion Movement

        #endregion Mechanics

        #region Setup

        private void Awake()
        {
            Setup();
            SetupStateMachine();
#if UNITY_EDITOR
            SetupEditorInfo();
#endif
        }

        private void Setup()
        {
            #region Get Main Components

            //Get necessary components//
            CapsuleCollider playerCollider = GetComponent<CapsuleCollider>();
            Rigidbody playerRigidBody = GetComponent<Rigidbody>();

            //Find Object that holds all player mechanics controllers//
            GameObject mechanicsHolder = GameObject.FindWithTag("PlayerMechanics");

            #endregion Get Main Components

            #region Setup Core

            //Input//
            _input = InputManager.Instance.CurrentInput;

            //Components Manager//
            _componentsManager = new PlayerComponentsManager(playerCollider, playerRigidBody);

            //Modifiers//
            _modifiers = new PlayerModifiers();

            //Possibilities//
            _possibilities = new PlayerPossibilities();

            //Cases//
            _cases = new PlayerCases();

            //Actions//
            _actions = new PlayerActions();

            #endregion Setup Core

            SetupMechanics(mechanicsHolder);
        }

        private void SetupMechanics(GameObject mechanicsHolder)
        {
            #region Rotation

            //Default//
            _rotationDefault = mechanicsHolder.GetComponent<PlayerRotationDefault>();
            _rotationDefault.Setup(_input, SettingsManager.Instance.CurrentSettings.Control, _possibilities.rotation, _cases.rotation);

            #endregion Rotation

            #region Movement

            //Default//
            _movementDefault = mechanicsHolder.GetComponent<PlayerMovementDefault>();
            _movementDefault.Setup(_input, _componentsManager, _modifiers.Movement, _possibilities.movement, _cases.movement, _actions.Movement);

            #endregion Movement
        }

        private void SetupStateMachine()
        {
            //Setup context//
            _stateContext = new PlayerStateContext(this, _rotationDefault, _movementDefault);

            //Factory//
            _stateFactory = new PlayerStateFactory(_stateContext);

            //Set first state//
            _stateContext.CurrentState = _stateFactory.Default();
            _stateContext.CurrentState.EnterStates();
        }

#if UNITY_EDITOR
        private void SetupEditorInfo()
        {
            CurrentInfo.currentPossibilities = _possibilities;
            CurrentInfo.currentCases = _cases;
        }
#endif

        #endregion Setup

        #region Case Checks

        private const float MinimalMoveThreshold = 0.05f;
        private Vector3 _positionLastFrame;

        private void UpdateIfIsChangingPosition()
        {
            Vector3 currentPosition = transform.position;

            _cases.movement.isChangingPosition = (currentPosition - _positionLastFrame).magnitude > MinimalMoveThreshold;

            _positionLastFrame = currentPosition;
        }

        #endregion Case Checks

        #region Update methodes

        private void Update()
        {
            //Update Cases//
            UpdateIfIsChangingPosition();

            //Update States//
            _stateContext.CurrentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            //Update States//
            _stateContext.CurrentState.FixedUpdateStates();
        }

        #endregion Update methodes
    }
}