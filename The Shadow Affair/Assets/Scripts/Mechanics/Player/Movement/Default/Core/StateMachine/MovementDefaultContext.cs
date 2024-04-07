using System;
using SmugRag.Templates.StateMachines;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultContext : StateContext
    {
        //Mechanic base//
        public MovementDefault Base { get; private set; }

        //Rigid Body//
        public Rigidbody RigidBody { get; private set; }

        //Actions//
        public MovementDefaultActions Actions { get; private set; }

        //Possibilities//
        public MovementDefaultPossibilitiesData Possibilities { get; private set; }

        //Input//
        public MovementDefaultInput Input { get; private set; }

        //Cases//
        public MovementDefaultCasesData Cases { get; private set; }

        //Checkers//
        public MovementDefaultCheckers Checkers { get; private set; }

        //Modifiers//
        public MovementDefaultModifiersData Modifiers { get; private set; }

        //Head Bobbing//
        public MovementDefaultHeadBobbing HeadBobbing { get; private set; }

        //Execution//
        public MovementDefaultExecution Execution { get; private set; }

        //Component Controllers//
        public MovementDefaultComponentsSetter ComponentsSetter { get; private set; }

        public void Setup(MovementDefault movementBase, Rigidbody rigidBody, MovementDefaultActions actions, MovementDefaultPossibilitiesData possibilities, MovementDefaultInput input, MovementDefaultCasesData cases, MovementDefaultCheckers checkers, MovementDefaultModifiersData modifiers, MovementDefaultHeadBobbing headBobbing, MovementDefaultExecution execution, MovementDefaultComponentsSetter componentsSetter)
        {
            Base = movementBase;
            RigidBody = rigidBody;
            Actions = actions;
            Possibilities = possibilities;
            Input = input;
            Cases = cases;
            Checkers = checkers;
            Modifiers = modifiers;
            HeadBobbing = headBobbing;
            Execution = execution;
            ComponentsSetter = componentsSetter;
        }
    }
}