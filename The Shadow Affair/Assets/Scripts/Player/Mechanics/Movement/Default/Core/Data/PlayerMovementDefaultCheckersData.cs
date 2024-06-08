using System;
using UnityEngine;

namespace SmugRagGames.Player.Movement
{
    [Serializable]
    public class PlayerMovementDefaultCheckersData
    {
        #region Debug Gizmos

#if UNITY_EDITOR

        [Header("Debug Gizmos")]
        public DebugGizmosClass debugGizmos;

        [Serializable]
        public class DebugGizmosClass
        {
            [Header("Ground Check")]
            public bool groundCheckDraw;

            public Color groundCheckGizmoColor;

            [Header("Slope Check")]
            public bool slopeCheckDraw;

            public Color slopeCheckGizmoColor;

            [Header("Celling Check")]
            public bool cellingCheckDraw;

            public Color cellingCheckGizmoColor;
        }

#endif

        #endregion Debug Gizmos
    }
}