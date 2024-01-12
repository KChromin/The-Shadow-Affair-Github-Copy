using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    //[CreateAssetMenu(fileName = "MovementDefaultScriptableObject", menuName = "ScriptableObjects/DefaultMovement")]
    public class MovementDefaultScriptableObject : ScriptableObject
    {
        [field: Header("Movement Factors")]
        [field: SerializeField]
        public MovementDefaultFactors movementFactors;

        [field: Space, Header("Checkers Parameters")]
        [field: SerializeField]
        public MovementDefaultCheckersParameters checkerParameters;
    }
}