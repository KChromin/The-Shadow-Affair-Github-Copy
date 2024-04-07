using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    [Serializable]
    public class MovementDefaultFootstepsData
    {
        [Header("Footsteps")]
        public EventReference footstepsDefault;
        [Space]
        public EventReference footstepsConcreteWalk;
        public EventReference footstepsConcreteRun;
        [Space]
        public EventReference footstepsWoodWalk;
        public EventReference footstepsWoodRun;
        [Space]
        public EventReference footstepsGrass;
        [Space]
        public EventReference footstepsGravel;
        [Space]
        public EventReference footstepsStone;

        [Header("Landing")]
        public EventReference landingDefault;
        [Space]
        public EventReference landingConcrete;
        [Space]
        public EventReference landingWood;
        [Space]
        public EventReference landingGrass;
        [Space]
        public EventReference landingGravel;
        [Space]
        public EventReference landingStone;

        [Header("Footsteps Offset")]
        public Vector3 footstepsOffsetXZ;
    }
}