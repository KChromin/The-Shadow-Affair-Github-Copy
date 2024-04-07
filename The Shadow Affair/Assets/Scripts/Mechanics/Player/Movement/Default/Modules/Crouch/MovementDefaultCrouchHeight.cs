using System;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCrouchHeight
    {
        private MovementDefaultCrouchHeightData _heightData;
        private MovementDefaultActions _actions;
        private MovementDefaultInput _input;

        //Head//
        private Transform _head;
        private Vector3 _headDefaultLocalPosition;

        //Body//
        private CapsuleCollider _collider;
        private float _colliderDefaultHeight;

        private Vector3 _headOffsetCalculation;

        private bool _isCrouching;
        private bool _wasIdle;
        private bool _colliderHeightIsUpdated;
        private bool _cameraOffsetIsUpdated;

        private const float ValueSnappingOffset = 0.002f;

        public void Setup(MovementDefaultCrouchHeightData crouchHeightData, Transform head, CapsuleCollider collider, MovementDefaultActions actions, MovementDefaultInput input)
        {
            _heightData = crouchHeightData;

            _head = head;
            _headDefaultLocalPosition = _head.localPosition;

            _collider = collider;
            _colliderDefaultHeight = _collider.height;

            _actions = actions;
            _input = input;
            SubscribeToEvents();
        }

        public void Update()
        {
            if (_colliderHeightIsUpdated && _cameraOffsetIsUpdated)
                return;

            if (_isCrouching)
            {
                if (_input.InputMove)
                {
                    if (_wasIdle)
                    {
                        _wasIdle = false;
                        HeadOffsetUpdate(_heightData.headPositionCrouchOffsetWalk, _heightData.headRepositionSpeedCrouchIdleToWalk);
                    }
                    else
                    {
                        HeadOffsetUpdate(_heightData.headPositionCrouchOffsetWalk, _heightData.headRepositionSpeedToCrouch);
                    }

                    ColliderHeightUpdate(_heightData.colliderHeightCrouchWalk);
                }
                else
                {
                    _wasIdle = true;
                    HeadOffsetUpdate(_heightData.headPositionCrouchOffsetIdle, _heightData.headRepositionSpeedCrouchWalkToIdle);
                    ColliderHeightUpdate(_heightData.colliderHeightCrouchIdle);
                }
            }
            else
            {
                HeadOffsetUpdate(Vector3.zero, _heightData.headRepositionSpeedToStand);
                ColliderHeightUpdate(_colliderDefaultHeight);
            }
        }

        #region Value Updates

        private void HeadOffsetUpdate(Vector3 newHeadOffset, float headOffsetTransitionTime)
        {
            Vector3 headLocalPosition = _head.localPosition;
            Vector3 target = _headDefaultLocalPosition + newHeadOffset;

            //Update Position//
            _head.localPosition = Vector3.SmoothDamp(headLocalPosition, target, ref _headOffsetCalculation, headOffsetTransitionTime);

            //Check if all axis reached tolerance points//
            if (Math.Abs(_head.localPosition.x - target.x) < ValueSnappingOffset && Math.Abs(_head.localPosition.y - target.y) < ValueSnappingOffset && Math.Abs(_head.localPosition.z - target.z) < ValueSnappingOffset)
            {
                _head.localPosition = target;
                _cameraOffsetIsUpdated = true;
            }
        }

        private void HeadOffsetUpdateInstant(Vector3 newHeadOffset)
        {
            _head.localPosition = newHeadOffset;
            _cameraOffsetIsUpdated = true;
        }

        private void ColliderHeightUpdate(float newColliderHeight)
        {
            if (Math.Abs(_collider.height - newColliderHeight) < ValueSnappingOffset) return;

            _collider.height = newColliderHeight;
            _collider.center = new Vector3(0, newColliderHeight / 2, 0);
        }

        #endregion Value Updates

        #region New State

        private void SetNewStateCrouch()
        {
            SetNewState(true);
        }

        private void SetNewStateStand()
        {
            SetNewState(false);
        }

        private void SetNewState(bool isCrouching)
        {
            _isCrouching = isCrouching;

            _colliderHeightIsUpdated = false;
            _cameraOffsetIsUpdated = false;
        }

        private void ResetState()
        {
            _wasIdle = false;
            ColliderHeightUpdate(_colliderDefaultHeight);
            HeadOffsetUpdateInstant(_headDefaultLocalPosition);
        }

        #endregion New State

        #region Event Subscriptions

        private void SubscribeToEvents()
        {
            _actions.OnCrouchAction += SetNewStateCrouch;
            _actions.OnStandUpAction += SetNewStateStand;
            _actions.OnDisableAction += ResetState;
        }

        public void UnsubscribeFromEvents()
        {
            _actions.OnCrouchAction -= SetNewStateCrouch;
            _actions.OnStandUpAction -= SetNewStateStand;
            _actions.OnDisableAction -= ResetState;
        }

        #endregion Event Subscriptions
    }
}