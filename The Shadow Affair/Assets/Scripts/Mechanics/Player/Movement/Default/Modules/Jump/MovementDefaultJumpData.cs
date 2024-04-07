using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultJumpData
    {
        [Header("Jump Force")]
        public float jumpForce = 3f;

        [Header("Cooldown Time")]
        public float jumpCooldown = 0.5f;
    }
}