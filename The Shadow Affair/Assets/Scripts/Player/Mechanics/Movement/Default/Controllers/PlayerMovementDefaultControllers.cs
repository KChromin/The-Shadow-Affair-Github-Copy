using System;
using SmugRagGames.Player.Movement.Input;
using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    [Serializable]
    public class PlayerMovementDefaultControllers
    {
        public PlayerMovementDefaultControllers(Rigidbody rigidBody, Transform orientation, PlayerMovementInput input, PlayerMovementDefaultScriptableObject parameters, PlayerMovementDefaultCheckers checkers, PlayerMovementDefaultCheckersScriptableObject checkersParameters, PlayerMovementModifiers modifiers, PlayerMovementCases cases, PlayerMovementPossibilities possibilities)
        {
            Run = new PlayerMovementDefaultRun(input, cases, possibilities);
            Execution = new PlayerMovementDefaultExecution(rigidBody, parameters, input, checkers, modifiers.CurrentModifiers, orientation);
            StairsHandler = new PlayerMovementDefaultStairsHandler(orientation, input, parameters, checkersParameters, checkers, Execution);
        }

        public readonly PlayerMovementDefaultRun Run;
        
        //Execution//
        public readonly PlayerMovementDefaultExecution Execution;

        //Stairs Handler//
        public readonly PlayerMovementDefaultStairsHandler StairsHandler;
    }
}