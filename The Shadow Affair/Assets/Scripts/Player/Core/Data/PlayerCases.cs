using System;
using SmugRagGames.Player.Movement;
using SmugRagGames.Player.Rotation;
using UnityEngine;

namespace SmugRagGames.Player
{
    [Serializable]
    public class PlayerCases
    {
        [Header("Rotation")]
        public PlayerRotationCases rotation = new();

        [Header("Movement")]
        public PlayerMovementCases movement = new();
    }
}