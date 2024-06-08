using System;
using UnityEngine;

namespace SmugRagGames.Player.Movement
{
    public class PlayerMovementActions
    {
        #region Default Movement

        //Grounding//
        public event Action OnGroundingAction;
        public event Action<float> OnLandingAction;
        public event Action OnAirborneAction;

        //Jump//
        public event Action OnJumpAction;

        //Run//
        public event Action OnRunStartAction;
        public event Action OnRunEndAction;

        //Crouching//
        public event Action OnCrouchAction;
        public event Action OnStandUpAction;

        #endregion Default Movement

        #region Invoke methodes

        #region Default Movement

        //Grounding//
        public void InvokeOnGroundingAction()
        {
            OnGroundingAction?.Invoke();
        }

        public void InvokeOnLandingAction(float landingForce)
        {
            OnLandingAction?.Invoke(landingForce);
        }

        public void InvokeOnAirborneAction()
        {
            OnAirborneAction?.Invoke();
        }

        //Jump//
        public void InvokeOnJumpAction()
        {
            OnJumpAction?.Invoke();
        }

        //Run//
        public void InvokeOnRunAction()
        {
            OnRunStartAction?.Invoke();
        }

        public void InvokeOnRunEndAction()
        {
            OnRunEndAction?.Invoke();
        }

        //Crouch//
        public void InvokeOnCrouchAction()
        {
            OnCrouchAction?.Invoke();
        }

        public void InvokeOnStandUpAction()
        {
            OnStandUpAction?.Invoke();
        }

        #endregion Default Movement

        #endregion Invoke methodes
    }
}