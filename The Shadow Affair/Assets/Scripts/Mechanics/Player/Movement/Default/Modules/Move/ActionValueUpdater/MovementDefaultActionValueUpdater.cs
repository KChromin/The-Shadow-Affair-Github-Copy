using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultActionValueUpdater
    {
        private Rigidbody _rigidBody;
        private MovementDefaultActions _actions;

        public void Setup(Rigidbody rigidBody, MovementDefaultActions actions)
        {
            _rigidBody = rigidBody;
            _actions = actions;
            
            SubscribeToEvents();
        }

        #region Public parameters

        public Vector3 OnAirborneMomentum { get; private set; }

        #endregion Public parameters


        #region Methodes

        private void UpdateOnAirborneMomentum()
        {
            OnAirborneMomentum = _rigidBody.velocity;
        }


        #endregion Methodes


        #region Event Action Subscribes

        private void SubscribeToEvents()
        {
            _actions.OnAirborneAction += UpdateOnAirborneMomentum;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnAirborneAction -= UpdateOnAirborneMomentum;
        }

        #endregion Event Action Subscribes
    }
}