using System;
using UnityEngine;

namespace SmugRagGames.Player.Movement
{
    [Serializable]
    public class PlayerMovementPossibilities
    {
        [Header("General")]
        public bool canMove = true;

        [Header("Default")]
        public bool canRun = true;
        public bool canCrouch = true;
        public bool canJump = true;
        
    }
}