using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultCrouchHeightData
    {
        [Header("Collider Height")]
        public float colliderHeightCrouchWalk;
        public float colliderHeightCrouchIdle;
        
        [Header("Camera Position")]
        public Vector3 headPositionCrouchOffsetWalk;
        public Vector3 headPositionCrouchOffsetIdle;
        [Space]
        public float headRepositionSpeedToCrouch = 0.2f;
        [Space]
        public float headRepositionSpeedCrouchIdleToWalk = 0.2f;
        public float headRepositionSpeedCrouchWalkToIdle = 0.2f;
        [Space]
        public float headRepositionSpeedToStand = 0.15f;

    }
}