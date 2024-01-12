using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRag.Templates.StateMachines
{
    public abstract class StateContext
    {
        public StateBase CurrentState { get; set; }
    }
}