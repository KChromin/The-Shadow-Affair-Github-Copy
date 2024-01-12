using System;
using SmugRag.Mechanics.Player.Movement.Default;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Templates.StateMachines
{
    [Serializable]
    public class MovementDefaultContext : StateContext
    {
        //Mechanic base//
        public MovementDefault MovementBase { get; private set; }

        //Checkers//
        public MovementDefaultCheckers MovementDefaultCheckers { get; private set; }

        //Execution//
        public MovementDefaultExecution Execution { get; private set; }

        public void Setup(MovementDefault movementBase, MovementDefaultCheckers movementDefaultCheckers, MovementDefaultExecution movementExecution)
        {
            MovementBase = movementBase;
            MovementDefaultCheckers = movementDefaultCheckers;
            Execution = movementExecution;
        }
    }
}