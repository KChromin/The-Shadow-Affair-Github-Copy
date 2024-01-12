using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultCheckersParameters
    {
        [Header("General")]
        public LayerMask groundLayers;

        [Space, Header("Ground Check")]
        public float groundCheckDistance = 0.68f;
        public float groundCheckRadius = 0.27f;

        [Space, Header("Slope Check")]
        public float slopeCheck;

        [Header("Slope Check Angles")]
        public float slopeCheckMinimumOnSlopeAngle = 5f;
        public float slopeCheckMaximumSlopeAngle = 50f;

        // [Space, Header("Celling Check")]

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
            public Color groundCheckRayColor = Color.red;
            public Color groundCheckRayColorHit = Color.green;
            public Color groundCheckSphereColor = Color.red;
            public Color groundCheckSphereColorHit = Color.green;

            [Space, Header("Slope Check")]
            public bool slopeCheck;

            [Space, Header("Celling Check")]
            public bool cellingCheck;
        }
#endif

        #endregion Debug Gizmos
    }
}