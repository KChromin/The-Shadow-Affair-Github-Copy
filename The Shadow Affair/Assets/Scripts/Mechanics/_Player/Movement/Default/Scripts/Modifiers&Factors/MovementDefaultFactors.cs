using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultFactors
    {
        [Header("Backwards Movement")]
        [Range(0f, 1f)]
        public float backwardsMovement = 0.8f;

        [Header("Sideways Movement")]
        [Range(0f, 1f)]
        public float sidewaysMovement = 0.9f;
    }
}