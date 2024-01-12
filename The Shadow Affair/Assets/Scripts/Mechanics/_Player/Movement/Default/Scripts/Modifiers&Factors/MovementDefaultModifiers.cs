using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultModifiers
    {
        [Header("Move Speed")]
        public float moveSpeed = 1;
        
        [Header("Run Speed")]
        public float runSpeed = 1;
        
        [Header("Jump Power")]
        public float jumpPower = 1;
    }
}
