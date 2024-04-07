using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultHeadBobbingData
    {
        [Space, Header("Idle")]
        public float bobbingAmplitudeIdle;
        public float bobbingFrequencyIdle = 2;
        public float bobbingTransitionToIdle = 0.1f;

        [Space, Header("Walk")]
        public float bobbingAmplitudeWalk;
        public float bobbingFrequencyWalk = 0.8f;
        public float bobbingTransitionToWalk = 0.2f;

        [Space, Header("Run")]
        public float bobbingAmplitudeRun;
        public float bobbingFrequencyRun = 2;
        public float bobbingTransitionToRun = 0.2f;

        [Space, Header("Crouch Idle")]
        public float bobbingAmplitudeCrouchIdle;
        public float bobbingFrequencyCrouchIdle = 2;
        public float bobbingTransitionToCrouchIdle = 0.2f;

        [Space, Header("Crouch Walk")]
        public float bobbingAmplitudeCrouchWalk;
        public float bobbingFrequencyCrouchWalk = 2;
        public float bobbingTransitionToCrouchWalk = 0.2f;

        [Space, Header("Landing Shake")]
        public float landingAmplitudeShake;
        public float landingFrequencyShake = 0.1f;
        public float landingVelocityWhenShakeHaveMaximalEffect = 26f;
    }
}