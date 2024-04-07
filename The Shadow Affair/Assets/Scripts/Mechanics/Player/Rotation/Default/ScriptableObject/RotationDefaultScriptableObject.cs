using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Rotation.Default
{
    [CreateAssetMenu(fileName = "RotationDefaultScriptableObject", menuName = "ScriptableObjects/DefaultRotation")]
    public class RotationDefaultScriptableObject : ScriptableObject
    {
        [Header("Look Parameters")]
        [SerializeField, Range(0, 90)]
        public int lookMaximalAngleUp = 80;

        [SerializeField, Range(0, 90)]
        public int lookMaximalAngleDown = 80;

        [Header("Look Smoothing")]
        [SerializeField, Range(0, 0.05f)]
        public float lookSmoothingSpeed = 0.02f;
    }
}