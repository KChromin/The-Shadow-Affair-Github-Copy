using System;
using UnityEngine;

namespace SmugRagGames.Player.Movement
{
    [Serializable]
    public class PlayerMovementCases
    {
        /// <summary> If player object changing position between frames </summary>
        [Header("General")]
        public bool isChangingPosition;

        /// <summary> If player gives input, and player is moving using methods </summary>
        public bool isMoving;

        [Header("Default")]
        public bool isRunning;
        public bool isCrouching;
        public bool isInJump; //After jump when still ascending//
    }
}