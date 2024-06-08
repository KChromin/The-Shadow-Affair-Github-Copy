using UnityEngine;
using SmugRagGames.Player.Movement;

namespace SmugRagGames.Player
{
    public class PlayerActions
    {
        public PlayerActions()
        {
            Movement = new PlayerMovementActions();
        }
        
        public PlayerMovementActions Movement;
    }
}