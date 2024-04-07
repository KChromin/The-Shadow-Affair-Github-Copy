using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultActions
    {
        //On Disable//
        public event Action OnDisableAction;

        //Grounding//
        public event Action OnGroundingAction;
        public event Action<float> OnLandingAction;
        public event Action OnAirborneAction;

        //Jump//
        public event Action OnJumpAction;

        //Run//
        public event Action OnRunAction;
        public event Action OnRunEndAction;

        //Crouching//
        public event Action OnCrouchAction;
        public event Action OnStandUpAction;
        public event Action OnFromCrouchToStandAfterJumpAction;
        public event Action OnCrouchToRunAction;

        //Footstep//
        public event Action<bool> OnFootstepAction; //Used to sounding footsteps, Triggered from Head Bobbing//

        #region Invoke Methodes

        #region OnDisable

        public void InvokeOnDisableAction()
        {
            OnDisableAction?.Invoke();
        }

        #endregion OnDisable

        #region Grounding

        public void InvokeOnGroundingAction()
        {
            OnGroundingAction?.Invoke();
        }

        public void InvokeOnLandingAction(float yVelocity)
        {
            OnLandingAction?.Invoke(yVelocity);
        }

        public void InvokeOnAirborneAction()
        {
            OnAirborneAction?.Invoke();
        }

        #endregion Grounding

        #region Jump

        public void InvokeOnJumpAction()
        {
            OnJumpAction?.Invoke();
        }

        #endregion Jump

        #region Run

        public void InvokeOnRunAction()
        {
            OnRunAction?.Invoke();
        }

        public void InvokeOnRunEndAction()
        {
            OnRunEndAction?.Invoke();
        }

        #endregion Run

        #region Crouch

        public void InvokeOnCrouchAction()
        {
            OnCrouchAction?.Invoke();
        }

        public void InvokeOnStandUpAction()
        {
            OnStandUpAction?.Invoke();
        }

        public void InvokeOnFromCrouchToStandAfterJumpAction()
        {
            OnFromCrouchToStandAfterJumpAction?.Invoke();
        }

        public void InvokeOnCrouchToRunAction()
        {
            OnCrouchToRunAction?.Invoke();
        }

        #endregion Crouch

        #region Footstep

        public void InvokeOnFootstepAction(bool isItRightFoot)
        {
            OnFootstepAction?.Invoke(isItRightFoot);
        }

        #endregion Footstep

        #endregion Invoke Methodes
    }
}