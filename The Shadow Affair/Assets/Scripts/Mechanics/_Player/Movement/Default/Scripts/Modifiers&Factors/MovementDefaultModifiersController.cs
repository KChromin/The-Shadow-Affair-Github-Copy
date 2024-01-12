using UnityEngine;
using UnityEngine.UIElements;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultModifiersController
    {
        [field: Header("Movement Modifiers")]
        public MovementDefaultModifiers CurrentMovementModifiers { get; private set; }

        public void Setup()
        {
            CurrentMovementModifiers = new MovementDefaultModifiers();
        }
        
    }
}