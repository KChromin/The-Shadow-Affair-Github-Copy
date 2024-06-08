using System;
using UnityEngine;

namespace SmugRagGames.Player.Rotation
{
    [Serializable]
    public class PlayerRotationPossibilities
    {
        [Header("General")]
        public bool canRotate = true;

        public bool canRotateY = true;
        public bool canRotateX = true;
    }
}