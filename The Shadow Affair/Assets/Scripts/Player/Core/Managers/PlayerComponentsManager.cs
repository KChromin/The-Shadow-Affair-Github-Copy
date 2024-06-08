using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRagGames.Player
{
    public class PlayerComponentsManager
    {
        public PlayerComponentsManager(CapsuleCollider collider, Rigidbody rigidBody)
        {
            _collider = collider;
            _rigidBody = rigidBody;

            _startingColliderHeight = _collider.height;

            SetupCustomRigidBodySettings();
        }

        //Components//
        private readonly CapsuleCollider _collider;
        private readonly Rigidbody _rigidBody;

        private float _startingColliderHeight;

        //Update in time//
        private bool _updateInTime;
        private float _targetHeight;
        private float _targetHeightCalculation;
        private float _targetCenterY;
        private float _targetCenterYCalculation;
        private float _targetTime;

        private const float RoundingValue = 0.01f;

        //Custom RigidBody Settings/
        private const byte CustomSolverIterationCount = 26;
        private const byte CustomSolverVelocityIterationCount = 10;
        private const byte CustomMaxLinearVelocity = 200;

        #region General methodes

        private void SetupCustomRigidBodySettings()
        {
            _rigidBody.solverIterations = CustomSolverIterationCount;
            _rigidBody.solverVelocityIterations = CustomSolverVelocityIterationCount;
            _rigidBody.maxLinearVelocity = CustomMaxLinearVelocity;
        }

        public void UpdateValuesInTime()
        {
            if (!_updateInTime) return;

            //Height//
            if (!Mathf.Approximately(_collider.height, _targetHeight))
            {
                Mathf.SmoothDamp(_collider.height, _targetHeight, ref _targetHeightCalculation, _targetTime);

                if (Mathf.Abs(_collider.height - _targetHeight) <= RoundingValue)
                {
                    _collider.height = _targetHeight;
                }
            }

            //Center//
            if (!Mathf.Approximately(_collider.center.y, _targetCenterY))
            {
                Vector3 currentCenter = _collider.center;

                Mathf.SmoothDamp(currentCenter.y, _targetCenterY, ref _targetCenterYCalculation, _targetTime);

                if (Mathf.Abs(currentCenter.y - _targetCenterY) <= RoundingValue)
                {
                    currentCenter.y = _targetCenterY;
                }

                _collider.center = currentCenter;
            }

            //If both are ready, stop updating//
            if (Mathf.Approximately(_collider.height, _targetHeight) && Mathf.Approximately(_collider.center.y, _targetCenterY))
            {
                _updateInTime = false;
            }
        }

        #endregion General methodes

        #region Collider methodes

        //Only 3 parameters need change - PhysicsMaterial, Height//

        public void UpdateColliderHeight(float newHeight)
        {
            _targetHeight = newHeight;

            _updateInTime = true;
        }

        public void UpdateColliderPhysicsMaterial(PhysicsMaterial newMaterial)
        {
            _collider.material = newMaterial;
        }

        #endregion Collider methodes

        #region Rigidbody methodes

        //Only Drag need update//

        public void UpdateRigidBodyDrag(float newValue)
        {
            _rigidBody.linearDamping = newValue;
        }

        #endregion Rigidbody methodes

        #region Data methodes

        public float GetCurrentCapsuleColliderHeight()
        {
            return _collider.height;
        }

        #endregion Data methodes
    }
}