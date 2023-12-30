using SmugRag.Templates.StateMachines;
using SmugRag.ScriptableObjects;
using UnityEngine;

namespace SmugRag.Player.Mechanics.Movement.Default
{
    public class MovementDefault : MonoBehaviour
    {
        [Header("Movement Properties")]
        [SerializeField]
        private MovementDefaultScriptableObject movementDefaultData;
        
        //State Machine Context//
        [Header("Context Data")]
        [SerializeField]
        private MovementDefaultContext contextData;
        private MovementDefaultStateFactory _stateFactory;

        private void Start()
        {
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            //Create new state factory//
            _stateFactory = new MovementDefaultStateFactory(contextData);

            //Set first state//
            contextData.currentState = _stateFactory.Grounded();
            contextData.currentState.EnterStates();
        }

        private void Update()
        {
            contextData.currentState.UpdateStates();
        }

        private void FixedUpdate()
        {
            contextData.currentState.FixedUpdateStates();
        }
    }
}