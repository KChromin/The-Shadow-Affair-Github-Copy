using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    //[CreateAssetMenu(fileName = "MovementDefaultScriptableObject", menuName = "ScriptableObjects/DefaultMovement")]
    public class MovementDefaultScriptableObject : ScriptableObject
    {
        [Header("Speed Parameters")]
        public MovementDefaultSpeedData speedData;

        [Header("Jump Parameters")]
        public MovementDefaultJumpData jumpData;

        [Header("Height Parameters")]
        public MovementDefaultCrouchHeightData crouchHeightData;

        [Header("Head Bobbing Parameters")]
        public MovementDefaultHeadBobbingData headBobbingData;

        [Header("Footsteps Parameters")]
        public MovementDefaultFootstepsData footstepsData;
        
        [Header("Movement Factors")]
        public MovementDefaultFactorsData factorsData;

        [Header("Checkers Parameters")]
        public MovementDefaultCheckersParametersData checkerParametersData;

        [Header("Component Settings")]
        public MovementDefaultComponentsSetterData componentsSetterData;
    }
}