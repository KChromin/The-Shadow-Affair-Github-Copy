using UnityEngine;
using SmugRagGames.Player.Movement;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultManagers
    {
        public PlayerMovementDefaultManagers(PlayerComponentsManager componentsManager, Rigidbody rigidBody, PlayerMovementDefaultScriptableObject parameters)
        {
            Components = componentsManager;
            MovementDefaultComponents = new PlayerMovementDefaultComponentsManager(rigidBody, componentsManager, parameters);
        }

        public readonly PlayerComponentsManager Components;
        public readonly PlayerMovementDefaultComponentsManager MovementDefaultComponents;
    }
}