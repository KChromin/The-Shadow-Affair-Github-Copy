using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultSpeedData
    {
        [Header("Walk")]
        public AnimationCurve accelerationWalk = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public float maxSpeedWalk = 4.5f;

        [Header("Run")]
        public AnimationCurve accelerationRun = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public float maxSpeedRun = 8f;

        [Header("Crouch")]
        public AnimationCurve accelerationCrouch = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public float maxSpeedCrouch = 3f;

        [Header("In Air")]
        public AnimationCurve accelerationInAir = AnimationCurve.EaseInOut(0, 1, 1, 0);
        public float maxSpeedInAir = 1.5f;
        
    }
}