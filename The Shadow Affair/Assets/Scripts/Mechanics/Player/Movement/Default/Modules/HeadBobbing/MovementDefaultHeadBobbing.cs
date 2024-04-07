using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultHeadBobbing
    {
        //Transforms//
        private Transform _headBase;
        private Transform _headBaseHeadBobbing;

        //Data//
        private MovementDefaultHeadBobbingData _data;
        private MovementDefaultActions _actions;

        //Current Progress//
        private float _headBobCurrentProgress;

        //Amplitude//
        private float _headBobCurrentAmplitude;
        private float _headBobCurrentAmplitudeCalculation;

        //Frequency//
        private float _headBobCurrentFrequency;
        private float _headBobCurrentFrequencyCalculation;

        //Reset//
        private bool _applyReset;
        private const float SmoothResetSpeed = 0.08f;
        private Vector3 _smoothResetCalculation;

        //Footsteps//
        private float _footstepCurrentProgress;
        private bool _footstepOnRightFoot = true;
        private bool _triggerFootsteps = true;

        //Landing//
        private float _landingCurrentProgress;
        private float _landingCurrentIntensity;
        private bool _isLanding;

        #region Setup

        public void Setup(Transform headBase, Transform headBaseHeadBobbing, MovementDefaultHeadBobbingData data, MovementDefaultActions actions)
        {
            _headBase = headBase;
            _headBaseHeadBobbing = headBaseHeadBobbing;
            _data = data;
            _actions = actions;

            SubscribeToEvents();
        }

        #endregion Setup

        #region Public Methodes

        //Head bobbing movement//
        public void Execute(MovementDefaultHeadBobbingModes mode)
        {
            //Must always update, for footsteps to work//
            UpdateProgress();

            //Turn off HeadBobbing//
            if (false) return;

            switch (mode)
            {
                case MovementDefaultHeadBobbingModes.Idle:
                    ExecuteHeadBob(_data.bobbingFrequencyIdle, _data.bobbingAmplitudeIdle, _data.bobbingTransitionToIdle, false);
                    break;
                case MovementDefaultHeadBobbingModes.Walk:
                    ExecuteHeadBob(_data.bobbingFrequencyWalk, _data.bobbingAmplitudeWalk, _data.bobbingTransitionToWalk);
                    break;
                case MovementDefaultHeadBobbingModes.Run:
                    ExecuteHeadBob(_data.bobbingFrequencyRun, _data.bobbingAmplitudeRun, _data.bobbingTransitionToRun);
                    break;
                case MovementDefaultHeadBobbingModes.CrouchIdle:
                    ExecuteHeadBob(_data.bobbingFrequencyCrouchIdle, _data.bobbingAmplitudeCrouchIdle, _data.bobbingTransitionToCrouchIdle);
                    break;
                case MovementDefaultHeadBobbingModes.CrouchWalk:
                    ExecuteHeadBob(_data.bobbingFrequencyCrouchWalk, _data.bobbingAmplitudeCrouchWalk, _data.bobbingTransitionToCrouchWalk);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public void SmoothReset()
        {
            Vector3 position = _headBaseHeadBobbing.localPosition;
            position = Vector3.SmoothDamp(position, Vector3.zero, ref _smoothResetCalculation, SmoothResetSpeed);
            _headBaseHeadBobbing.localPosition = position;
        }

        private void InstantRestart()
        {
            _applyReset = true;
            _headBobCurrentAmplitude = 0;
            _headBobCurrentFrequency = 0;
        }

        #endregion Public Methodes

        #region Private Methodes

        #region Head Bobbing

        private void ExecuteHeadBob(float bobbingFrequency, float bobbingAmplitude, float transitionTime, bool checkFootstep = true)
        {
            if (_applyReset) return;

            TransitionHeadBobbingValues(bobbingFrequency, bobbingAmplitude, transitionTime);

            Vector3 finalPosition = CalculateVectorHeadBobbing();

            _headBaseHeadBobbing.localPosition = finalPosition;

            _triggerFootsteps = checkFootstep;
            FootstepsTrigger();
        }

        private void UpdateProgress()
        {
            //Add time to progress//
            _headBobCurrentProgress += Time.deltaTime * _headBobCurrentFrequency;

            //Reset after 2 full cycles//
            if (_headBobCurrentProgress >= Mathf.PI * 4)
            {
                _headBobCurrentProgress -= Mathf.PI * 4;
            }

            //Add time to footstep Progress//
            _footstepCurrentProgress += Time.deltaTime * _headBobCurrentFrequency;
        }

        private void TransitionHeadBobbingValues(float bobbingFrequency, float bobbingAmplitude, float transitionTime)
        {
            //Frequency//
            _headBobCurrentFrequency = Mathf.SmoothDamp(_headBobCurrentFrequency, bobbingFrequency, ref _headBobCurrentFrequencyCalculation, transitionTime);

            //Amplitude//
            _headBobCurrentAmplitude = Mathf.SmoothDamp(_headBobCurrentAmplitude, bobbingAmplitude, ref _headBobCurrentAmplitudeCalculation, transitionTime);
        }

        private Vector3 CalculateVectorHeadBobbing()
        {
            Vector3 newVector = Vector3.zero;

            newVector.y = Mathf.Sin(_headBobCurrentProgress) * _headBobCurrentAmplitude;
            newVector.x = Mathf.Cos(_headBobCurrentProgress / 2) * (_headBobCurrentAmplitude * 2);

            return newVector;
        }

        #endregion Head Bobing

        #region Landing

        private void ExecuteLanding(float landingVelocity)
        {
            if (_isLanding) return;

            //Here FMOD Call//

            float intensityMultiplier = Mathf.Clamp(Mathf.Abs(landingVelocity) / _data.landingVelocityWhenShakeHaveMaximalEffect, 0, 1);

            _landingCurrentIntensity = _data.landingAmplitudeShake * intensityMultiplier;

            _isLanding = true;
        }

        public void UpdateLandingShake()
        {
            if (!_isLanding) return;

            _landingCurrentProgress += Time.deltaTime * _data.landingFrequencyShake;

            _headBase.localPosition = CalculateVectorLanding();

            //End of the cycle//
            if (_landingCurrentProgress >= Mathf.PI)
            {
                _headBase.localPosition = Vector3.zero;
                ResetLanding();
            }
        }

        private Vector3 CalculateVectorLanding()
        {
            Vector3 newVector = Vector3.zero;

            newVector.y = Mathf.Sin(Mathf.PI + _landingCurrentProgress) * _landingCurrentIntensity;

            return newVector;
        }

        private void ResetLanding()
        {
            _isLanding = false;
            _landingCurrentProgress = 0;
            _landingCurrentIntensity = 0;
        }

        #endregion Landing

        private void ResetProgressValue()
        {
            _headBobCurrentProgress = 0;
        }

        private void ResetFlags()
        {
            _applyReset = false;
        }

        private void FootstepsTrigger()
        {
            if (_footstepCurrentProgress >= Mathf.PI * 2)
            {
                _footstepCurrentProgress -= Mathf.PI * 2;

                if (_triggerFootsteps)
                {
                    _actions.InvokeOnFootstepAction(_footstepOnRightFoot);
                }

                _footstepOnRightFoot = !_footstepOnRightFoot;
            }
        }

        #endregion Private Methodes


        #region Event Subscriptions

        private void SubscribeToEvents()
        {
            _actions.OnAirborneAction += InstantRestart;
            _actions.OnGroundingAction += ResetFlags;
            _actions.OnLandingAction += ExecuteLanding;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnAirborneAction -= InstantRestart;
            _actions.OnGroundingAction -= ResetFlags;
            _actions.OnLandingAction -= ExecuteLanding;
        }

        #endregion Action Subscriptions
    }

    public enum MovementDefaultHeadBobbingModes
    {
        Idle,
        Walk,
        Run,
        CrouchIdle,
        CrouchWalk
    }
}