using System;
using SmugRagGames.Patterns.Singleton;
using UnityEngine;

namespace SmugRagGames.Managers.Input
{
    public class InputManager : SingletonPersistent<InputManager>
    {
        public GameControls CurrentInput { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            //Setup input actions//
            CurrentInput = new GameControls();
        }

        #region OnEnable OnDisable

        private void OnEnable()
        {
            CurrentInput?.Enable();
        }

        private void OnDisable()
        {
            CurrentInput?.Disable();
        }

        #endregion OnEnable OnDisable
    }
}