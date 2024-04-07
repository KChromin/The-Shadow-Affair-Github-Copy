using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Tags
{
    [AddComponentMenu("Tags/Footsteps Type Tag")]
    public class FootstepsTypeTag : MonoBehaviour
    {
        public FootstepsGroundType groundType;
    }
    
    public enum FootstepsGroundType
    {
        Default,
        Concrete,
        Wood,
        Grass,
        Gravel,
        Stone
    }
}