using UnityEngine;

namespace SmugRagGames.Player.Rotation
{
    [CreateAssetMenu(fileName = "PlayerRotationDefault", menuName = "ScriptableObjects/Player/Rotation/Default")]
    public class PlayerRotationDefaultScriptableObject : ScriptableObject
    {
        [Header("Rotation Angle Limits")]
        [Range(0, 90)]
        public byte maximalAngleUp = 80;

        [Range(0, 90)]
        public byte maximalAngleDown = 80;

        [Header("Rotation Smoothing Value")]
        [Range(0f, 0.1f)]
        public float smoothingSpeedValue = 0.02f;
    }
}