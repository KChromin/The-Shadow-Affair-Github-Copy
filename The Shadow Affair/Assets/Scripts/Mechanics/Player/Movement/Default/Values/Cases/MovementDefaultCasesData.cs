using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player
{
    [Serializable]
    public class MovementDefaultCasesData
    {
        [Header("Running")]
        public bool isRunning;
        
        [Header("Crouching")]
        public bool isCrouching;
        
        [Header("Jumped")]
        public bool jumped;
    }
}
