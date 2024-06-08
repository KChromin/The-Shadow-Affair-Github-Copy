using SmugRagGames.Patterns.StateMachine;
using SmugRagGames.Player.Movement.Input;
using SmugRagGames.Player.Movement;
using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStateContext : StateContext
    {
        public PlayerMovementDefaultStateContext(PlayerMechanicsBase controllerBase, PlayerMovementDefaultScriptableObject parameters, PlayerMovementInput input, PlayerMovementModifiers modifiers, PlayerMovementPossibilities possibilities, PlayerMovementCases cases, PlayerMovementActions actions,
            PlayerMovementDefaultCheckers checkers, PlayerMovementDefaultControllers controllers, PlayerMovementDefaultManagers managers, Rigidbody rigidBody)
        {
            Base = controllerBase;
            Parameters = parameters;
            Input = input;
            Modifiers = modifiers;
            Possibilities = possibilities;
            Cases = cases;
            Actions = actions;
            Checkers = checkers;
            Controllers = controllers;
            Managers = managers;
        }

        public readonly PlayerMechanicsBase Base;

        //Parameters//
        public readonly PlayerMovementDefaultScriptableObject Parameters;

        //Input for movement mechanics//
        public readonly PlayerMovementInput Input;

        //Modifiers//
        public readonly PlayerMovementModifiers Modifiers;

        //Possibilities//
        public readonly PlayerMovementPossibilities Possibilities;

        //Cases//
        public readonly PlayerMovementCases Cases;

        //Actions//
        public readonly PlayerMovementActions Actions;

        //Checkers//
        public readonly PlayerMovementDefaultCheckers Checkers;

        //Controllers//
        public readonly PlayerMovementDefaultControllers Controllers;

        //Manager//
        public readonly PlayerMovementDefaultManagers Managers;

        #region Editor Only

#if UNITY_EDITOR
        public PlayerMovementDefault.InfoClass CurrentInfo;

        public void SetupEditorSetup(PlayerMovementDefault.InfoClass currentInfo)
        {
            CurrentInfo = currentInfo;
        }
#endif

        #endregion Editor Only
    }
}