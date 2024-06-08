using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SmugRagGames.Player.Movement.Default
{
    [CreateAssetMenu(fileName = "PlayerMovementDefault", menuName = "ScriptableObjects/Player/Movement/Default")]
    public class PlayerMovementDefaultScriptableObject : ScriptableObject
    {
        #region Speeds

        [Header("Movement Speeds")]
        public MovementSpeedsClass movementSpeeds;

        [Serializable]
        public class MovementSpeedsClass
        {
            [Header("Walk")]
            public float maxSpeedWalk = 4f;

            [Header("Run")]
            public float maxSpeedRun = 7f;

            [Header("Crouch Walk")]
            public float maxSpeedCrouchWalk = 2f;

            [Header("In Air")]
            public float maxSpeedInAir = 1f;
        }

        #endregion Speeds

        #region Factors

        [Header("Movement Factors")]
        public MovementFactorsClass movementFactors;

        [Serializable]
        public class MovementFactorsClass
        {
            [Header("Backwards Movement")]
            [Range(0f, 1f)]
            public float backwardsMovement = 0.85f;

            [Header("Sideways Movement")]
            [Range(0f, 1f)]
            public float sidewaysMovement = 0.92f;

            //On landing speed reduction factor//
        }

        #endregion Factors

        #region Drags & Physics Materials

        [Header("Physics Parameters")]
        public PhysicsParametersClass physicsParameters;

        [Serializable]
        public class PhysicsParametersClass
        {
            [Header("Drags")]
            public float dragGrounded;
            public float dragInAir;

            [Header("Physics Materials")]
            public PhysicsMaterial materialIdle;
            public PhysicsMaterial materialIdleFullyStopped;
            public PhysicsMaterial materialMove;
            public PhysicsMaterial materialInAir;

            [Header("Anti Drag Values")]
            public readonly float AntiDragValueForDrag1 = 1.02f;
            public readonly float AntiDragValueForDrag7 = 8.14f;
            public readonly float AntiDragValueForDrag8 = 9.53f;
            public readonly float AntiDragValueForDrag9 = 11f;
            public readonly float AntiDragValueForDrag10 = 12.5f;

            [Header("Thresholds")]
            public float fullyStoppedMoveMagnitudeThreshold = 0.5f;

            [Header("Forces")]
            public float additionalGroundingForce = 1f;
            public float steepSlopeSlidingForce = 5f;
        }

        #endregion Drags & Physics Materials

        #region Stairs, Obstacles

        [Header("Stairs Handler Parameters")]
        public StairsHandlerParametersClass stairsHandlerParameters;

        [Serializable]
        public class StairsHandlerParametersClass
        {
            [Header("Step Height")]
            public float stepHeight = 0.3f;

            [Header("Minimal Step Height")]
            public float stepHeightMinimal = 0.05f;

            [Header("Step Up Force")]
            public float stepUpForceWalk = 6.5f;
            public float stepUpForceRun = 8f;
            public float stepUpForceCrouchWalk = 5f;
            
            [Header("Step Check Distance")]
            public float stepCheckDistanceHigh = 0.2f;
            public float stepCheckDistanceLow = 0.15f;

            [Header("Check Iterations")]
            public byte stepCheckIterationPerSide = 1;
            public float stepCheckIterationOffsetInDegrees = 15;
            
#if UNITY_EDITOR
            [Header("Debug Gizmos")]
            public bool stepCheckDrawGizmos;
#endif
        }

        #endregion Stairs, Obstacles
    }
}