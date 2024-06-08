using System;
using UnityEngine;
using SmugRagGames.Player.Movement;

namespace SmugRagGames.Player
{
    [Serializable]
    public class PlayerModifiers
    {
        public PlayerModifiers()
        {
            Movement = new PlayerMovementModifiers();
        }
        
        [Header("Movement")]
        public PlayerMovementModifiers Movement;
    }
}