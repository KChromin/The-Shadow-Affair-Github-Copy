using System;
using UnityEngine;
using SmugRag.Player.Mechanics.Checkers.Default;

namespace SmugRag.Templates.StateMachines
{
    [Serializable]
    public class MovementDefaultContext : StateContext
    {
        [field: SerializeField]
        public CheckersDefaultGround CheckersGround { get; private set; }
    }
}