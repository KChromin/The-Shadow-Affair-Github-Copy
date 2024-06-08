using UnityEngine;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultComponentsManager
    {
        public PlayerMovementDefaultComponentsManager(Rigidbody rigidBody, PlayerComponentsManager componentsManager, PlayerMovementDefaultScriptableObject parameters)
        {
            _rigidBody = rigidBody;
            _componentsManager = componentsManager;
            _parameters = parameters;
        }

        private readonly Rigidbody _rigidBody;
        private readonly PlayerComponentsManager _componentsManager;
        private readonly PlayerMovementDefaultScriptableObject _parameters;

        #region Public methodes

        //Idle State PhysicsMaterial Switcher//
        public void IdlePhysicsMaterialUpdater()
        {
            _componentsManager.UpdateColliderPhysicsMaterial(_rigidBody.linearVelocity.magnitude < _parameters.physicsParameters.fullyStoppedMoveMagnitudeThreshold ? _parameters.physicsParameters.materialIdleFullyStopped : _parameters.physicsParameters.materialIdle);
        }

        #endregion Public methodes
    }
}