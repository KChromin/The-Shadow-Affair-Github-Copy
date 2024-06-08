using System;
using SmugRagGames.Player.Movement.Input;
using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefault : PlayerMechanicsBase
    {
        #region Current Info

#if UNITY_EDITOR
        [Header("Movement Info")]
        public InfoClass info;

        [Serializable]
        public class InfoClass
        {
            #region Current State

            [Header("Current States")]
            public CurrentStatesClass currentStates;

            [Serializable]
            public class CurrentStatesClass
            {
                #region Grounding

                [Header("Grounding")]
                public MainStates mainState;

                public enum MainStates
                {
                    Disabled,
                    Grounded,
                    InAir
                }

                #endregion Grounding

                #region Grounded

                [Header("Grounded")]
                public GroundedStates groundedState;

                public enum GroundedStates
                {
                    Disabled,
                    Standing,
                    Crouching
                }

                #region Standing

                [Header("Standing")]
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

                [Header("Crouching")]
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

                [Header("In Air")]
                public InAirStates inAirState;

                public enum InAirStates
                {
                    Disabled,
                    Falling,
                    Rising
                }

                #endregion In Air
            }

            #endregion Current State

            [Header("Modifiers")]
            public PlayerMovementModifiers.ModifiersDataClass modifiers;
        }
#endif

        #endregion Current Info

        #region Parameters

        [Header("Parameters")]
        [SerializeField]
        private PlayerMovementDefaultScriptableObject parameters;

        [Header("Checkers Parameters")]
        [SerializeField]
        private PlayerMovementDefaultCheckersScriptableObject checkersParameters;

        #endregion Parameters

        #region Components

        [Header("RigidBody")]
        [SerializeField]
        private Rigidbody rigidBody;

        [Header("Orientation")]
        [SerializeField]
        private Transform orientation;

        #endregion Components

        #region Core

        //Input for movement mechanics//
        private PlayerMovementInput _input;

        //Checkers//
        private PlayerMovementDefaultCheckers _checkers;

        //Head Manager//

        //Modifiers//
        private PlayerMovementModifiers _modifiers;

        //Possibilities//
        private PlayerMovementPossibilities _possibilities;

        //Cases//
        private PlayerMovementCases _cases;

        //Actions//
        private PlayerMovementActions _actions;

        //State machine//
        //Context//
        private PlayerMovementDefaultStateContext _context;

        //Factory//
        private PlayerMovementDefaultStateFactory _factory;

        #endregion Core

        #region Controllers

        //Controllers//
        private PlayerMovementDefaultControllers _controllers;

        //Managers//
        private PlayerMovementDefaultManagers _managers;

        #endregion Controllers

        #region Setups

        public void Setup(GameControls input, PlayerComponentsManager componentsManagers, PlayerMovementModifiers modifiers, PlayerMovementPossibilities possibilities, PlayerMovementCases cases, PlayerMovementActions actions)
        {
            #region Setup Core

            //Input//
            _input = new PlayerMovementInput();
            _input.Setup(input);

            //Checkers//
            GameObject checkersObject = GameObject.FindWithTag("PlayerCheckers");
            _checkers = new PlayerMovementDefaultCheckers(checkersObject.transform, checkersParameters, componentsManagers.GetCurrentCapsuleColliderHeight());

            //Modifiers//
            _modifiers = modifiers;

            //Possibilities//
            _possibilities = possibilities;

            //Cases//
            _cases = cases;

            //Actions//
            _actions = actions;

            #endregion Setup Core

            //Managers//
            _managers = new PlayerMovementDefaultManagers(componentsManagers, rigidBody, parameters);

            //Controllers//
            _controllers = new PlayerMovementDefaultControllers(rigidBody, orientation, _input, parameters, _checkers, checkersParameters, _modifiers, _cases, _possibilities);

            SetupStateMachine();
        }


        private void SetupStateMachine()
        {
            //Context//
            _context = new PlayerMovementDefaultStateContext(this, parameters, _input, _modifiers, _possibilities, _cases, _actions, _checkers, _controllers, _managers, rigidBody);

            #region Setup Debug

#if UNITY_EDITOR
            _context.SetupEditorSetup(info);
#endif

            #endregion Setup Debug

            //Factory//
            _factory = new PlayerMovementDefaultStateFactory(_context);

            EnterStates();
        }

        private void EnterStates()
        {
            //Enter States//
            if (IsEnabled)
            {
                _context.CurrentState = _checkers.IsGrounded() ? _factory.Grounded() : _factory.InAir();
            }
            else
            {
                _context.CurrentState = _factory.Disabled();
            }

            _context.CurrentState.EnterStates();
        }

        #endregion Setups

        #region Methodes

        private void Update()
        {
            //Update Input//
            _input.UpdateData();

            //Update States//
            _context.CurrentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            //Update States//
            _context.CurrentState.FixedUpdateStates();
        }

        private void OnDisable()
        {
            //Disable All SubStates//
            _context.CurrentState = _factory.Disabled();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;

            _checkers.DrawGizmos();
        }
#endif

        //Called everytime something enabling, or disabling this mechanic//
        public override void ChangeEnabledness(bool newEnablednessState)
        {
            base.ChangeEnabledness(newEnablednessState);
            UpdateStatesByEnabledness();
        }

        private void UpdateStatesByEnabledness()
        {
            if (IsEnabled)
            {
                EnterStates();
            }
            else //Disable states//
            {
                _context.CurrentState = _factory.Disabled();
            }
        }

        #endregion Methodes
    }
}