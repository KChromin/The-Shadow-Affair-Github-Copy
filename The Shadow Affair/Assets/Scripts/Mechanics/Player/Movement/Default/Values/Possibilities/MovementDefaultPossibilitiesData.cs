using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultPossibilitiesData
    {
        [Header("Possibilities")]
        public bool canMove = true;
        public bool canRun = true;
        public bool canJump = true;
        public bool canCrouch = true;
    }
}