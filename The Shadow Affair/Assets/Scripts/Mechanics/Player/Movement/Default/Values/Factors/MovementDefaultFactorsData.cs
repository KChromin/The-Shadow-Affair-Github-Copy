using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultFactorsData
    {
        [Header("Backwards Movement")]
        [Range(0f, 1f)]
        public float backwardsMovement = 0.8f;

        [Header("Sideways Movement")]
        [Range(0f, 1f)]
        public float sidewaysMovement = 0.9f;

        [Header("On Landing Momentum Reduction")]
        [Range(0f, 1f)]
        public float landingMomentumReductionMin = 0.1f;
        [Range(0f, 1f)]
        public float landingMomentumReductionMax = 0.9f;
        [Range(0f, 100f)]
        public float velocityForMaximalReduction = 20f;

        [Header("Grounding Force")]
        [Range(0f, 3f)]
        public float groundingForceMultiplier = 1f;
        [Range(0f, 3f)]
        public float groundingForceSlopeMultiplier = 1f;
    }
}