using System;
using SmugRagGames.Player.Movement;
using SmugRagGames.Player.Rotation;
using UnityEngine;

namespace SmugRagGames.Player
{
    [Serializable]
    public class PlayerPossibilities
    {
        public PlayerPossibilities()
        {
            rotation = new PlayerRotationPossibilities();
            movement = new PlayerMovementPossibilities();
        }

        [Header("Rotation")]
        public PlayerRotationPossibilities rotation;

        [Header("Movement")]
        public PlayerMovementPossibilities movement;
    }
}