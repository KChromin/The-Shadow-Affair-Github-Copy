using System;
using UnityEngine;

namespace SmugRagGames.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementDefaultCheckers", menuName = "ScriptableObjects/Player/Movement/Checkers/Default")]
    public class PlayerMovementDefaultCheckersScriptableObject : ScriptableObject
    {
        [Header("Ground Layers")]
        public LayerMask groundLayers;

        #region Ground Check

        [Header("Ground Check")]
        public float groundCheckDistance = 0.7f;
        public float groundCheckRadius = 0.25f;

        #endregion Ground Check

        #region Slope Check

        [Space, Header("Slope Check")]
        public float slopeCheckSphereDistance = 0.7f;
        public float slopeCheckSphereRadius = 0.25f;
        [Space]
        public float slopeCheckRayDistance = 0.04f;
        public float slopeCheckRayOffsetY = 0.02f;
        
        
        [Header("Slope Angles")]
        public float slopeCheckAngleMinimum = 5f;
        public float slopeCheckAngleMaximum = 50f;

        #endregion Slope Check

        #region Celling Check

        [Space, Header("Celling Check")]
        public float cellingCheckDistanceMinimum = 0.75f; //Smallest the player can crouch//
        public float cellingCheckRadius = 0.25f;
        public float cellingCheckErrorMargin = 0.05f;

        #endregion Celling Check

        #region Debug Gizmos

#if UNITY_EDITOR
        [Header("Debug Gizmos")]
        public DebugGizmosClass debugGizmos;

        [Serializable]
        public class DebugGizmosClass
        {
            [Header("Ground Check")]
            public bool groundCheckDraw;

            public Color groundCheckColorTrue = Color.green;
            public Color groundCheckColorFalse = Color.red;

            [Header("Slope Check")]
            public bool slopeCheckDraw;

            public Color slopeCheckColorTrue = Color.green;
            public Color slopeCheckColorFalse = Color.red;

            [Header("Celling Check")]
            public bool cellingCheckDraw;

            public Color cellingCheckColorTrue = Color.green;
            public Color cellingCheckColorFalse = Color.red;
        }

#endif

        #endregion Debug Gizmos
    }
}