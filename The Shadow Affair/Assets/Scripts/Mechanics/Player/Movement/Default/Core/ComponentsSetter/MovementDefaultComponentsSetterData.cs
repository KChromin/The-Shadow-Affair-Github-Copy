using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultComponentsSetterData
    {
        #region Physics Materials

        [Header("Physics Materials")]
        public PhysicMaterial materialIdle;
        public PhysicMaterial materialMove;
        public PhysicMaterial materialInAir;
        
        public enum PhysicsMaterials
        {
            Idle,
            Move,
            InAir
        }

        #endregion Physics Materials

        #region Drag Values

        [Space, Header("Drag Values")]
        public float dragGrounded;
        public float dragInAir;

        public enum DragModes
        {
            Grounded,
            InAir
        }

        #endregion Drag Values
    }
}