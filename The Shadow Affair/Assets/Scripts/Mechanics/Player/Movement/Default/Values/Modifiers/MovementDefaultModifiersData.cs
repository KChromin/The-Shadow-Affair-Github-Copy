using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultModifiersData
    {
        [Header("Multipliers")]
        [Header("Move Speed")]
        [Range(0, 3)]
        public float moveSpeedMultiplier = 1;

        [Header("Run Speed")]
        [Range(0, 3)]
        public float runSpeedMultiplier = 1;

        [Header("Jump Force")]
        [Range(0, 10)]
        public float jumpForceMultiplier = 1;

        [Header("Gravity Force")]
        [Range(0, 10)]
        public float gravityForceMultiplier = 1;
    }
}