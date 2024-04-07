using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultCheckersParametersData
    {
        [Header("General")]
        public LayerMask groundLayers;

        [Space, Header("Ground Check")]
        public float groundCheckDistance = 0.68f;
        public float groundCheckRadius = 0.27f;

        [Space, Header("Slope Check")]
        public float slopeCheckSphereDistance = 0.68f;
        public float slopeCheckSphereRadius = 0.27f;
        public float slopeCheckRayDistance = 0.04f;
        public float slopeCheckRayOffsetY = 0.02f;

        [Header("Slope Check Angles")]
        public float slopeCheckMinimumOnSlopeAngle = 5f;
        public float slopeCheckMaximumSlopeAngle = 50f;

        [Space, Header("Celling Check")]
        public float cellingCheckRadius = 0.3f;
        public float cellingCheckDistanceErrorMargin = 0.01f;

        #region Debug Gizmos

#if UNITY_EDITOR
        [field: Space, Header("Debug Gizmos")]
        public DebugGizmosClass debugGizmos;

        [Serializable]
        public class DebugGizmosClass
        {
            [Header("Ground Check")]
            public bool groundCheck;
            [Space]
            public Color groundCheckColor = Color.red;
            public Color groundCheckColorHit = Color.green;

            [Space, Header("Slope Check")]
            public bool slopeCheck;
            [Space]
            public Color slopeCheckColor = Color.blue;
            public Color slopeCheckColorHit = Color.yellow;

            [Space, Header("Celling Check")]
            public bool cellingCheck;
            [Space]
            public Color cellingCheckColor = Color.red;
            public Color cellingCheckColorHit = Color.green;
        }
#endif

        #endregion Debug Gizmos
    }
}