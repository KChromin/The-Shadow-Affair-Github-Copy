using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultModifiers
    {
        [field: Header("Movement Modifiers")]
        public MovementDefaultModifiersData CurrentData { get; private set; }
    }
}