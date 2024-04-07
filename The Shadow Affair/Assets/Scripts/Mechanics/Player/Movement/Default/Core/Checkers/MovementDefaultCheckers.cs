using Unity.Collections;
using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCheckers
    {
        #region Parameters & Setup

        private Transform _origin;

        private MovementDefaultCheckersParametersData _parametersData;

        private Vector3 _slopeCheckRayOffset;
        private Vector3 _slopeCheckNormal;
        private float _cellingCheckDistance;

        private RaycastHit _groundHit;
        private RaycastHit _slopeHitSphere;
        private RaycastHit _slopeHitRay;
        private RaycastHit _cellingHit;

        private bool _currentSlopeIsTooSteep;

        public void Setup(MovementDefaultScriptableObject movementParameters, Transform origin, CapsuleCollider collider)
        {
            _parametersData = movementParameters.checkerParametersData;
            _origin = origin;

            //Slope Check//
            _slopeCheckRayOffset = new Vector3(0, _parametersData.slopeCheckRayOffsetY, 0);

            //Set celling check distance//
            float halfHeight = collider.height / 2;
            _cellingCheckDistance = (halfHeight - _parametersData.cellingCheckRadius) + _parametersData.cellingCheckDistanceErrorMargin;
        }

        #endregion Parameters & Setup

        #region Ground Check

        public bool IsGrounded()
        {
            return Physics.SphereCast(_origin.position, _parametersData.groundCheckRadius, Vector3.down, out _groundHit, _parametersData.groundCheckDistance, _parametersData.groundLayers, QueryTriggerInteraction.Ignore);
        }

        public RaycastHit GetGroundHit()
        {
            return _groundHit;
        }

        #endregion Ground Check

        #region Slope Check

        public bool IsOnSlope()
        {
            //Sphere Check//
            if (Physics.SphereCast(_origin.position, _parametersData.slopeCheckSphereRadius, Vector3.down, out _slopeHitSphere, _parametersData.slopeCheckSphereDistance, _parametersData.groundLayers, QueryTriggerInteraction.Ignore))
            {
                _slopeCheckNormal = _slopeHitSphere.normal;

                //Check Ray//
                if (Physics.Raycast(_slopeHitSphere.point + _slopeCheckRayOffset, Vector3.down, out _slopeHitRay, _parametersData.slopeCheckRayDistance, _parametersData.groundLayers, QueryTriggerInteraction.Ignore))
                {
                    _slopeCheckNormal = _slopeHitRay.normal;
                }
            }
            else
            {
                _slopeCheckNormal = _groundHit.normal;
            }

            return IsAngleInSlopeRange(_slopeCheckNormal);
        }

        private bool IsAngleInSlopeRange(Vector3 slopeHitNormal)
        {
            float hitAngle = Vector3.Angle(slopeHitNormal, Vector3.up);

            //If higher than minimum angle then on slope//
            if (_parametersData.slopeCheckMinimumOnSlopeAngle < hitAngle)
            {
                //If higher than maximum angle then slope is too steep//
                if (_parametersData.slopeCheckMaximumSlopeAngle < hitAngle)
                {
                    _currentSlopeIsTooSteep = true;
                }
                else
                {
                    _currentSlopeIsTooSteep = false;
                }

                return true;
            }
            else
            {
                _currentSlopeIsTooSteep = false;
            }

            return false;
        }

        public bool GetIfSlopeIsTooSteep()
        {
            return _currentSlopeIsTooSteep;
        }

        public Vector3 GetSlopeNormal()
        {
            return _slopeCheckNormal;
        }

        #endregion Slope Check

        #region Celling Check

        public bool HitCelling()
        {
            return Physics.SphereCast(_origin.position, _parametersData.groundCheckRadius, Vector3.up, out _cellingHit, _cellingCheckDistance, _parametersData.groundLayers, QueryTriggerInteraction.Ignore);
        }

        public RaycastHit GetCellingHit()
        {
            return _cellingHit;
        }

        #endregion Celling Check

        #region Debug Gizmo

#if UNITY_EDITOR

        public void DebugDrawGizmos()
        {
            if (_parametersData.debugGizmos.groundCheck)
            {
                DebugDrawGroundCheck();
            }

            if (_parametersData.debugGizmos.slopeCheck)
            {
                DebugDrawSlopeCheck();
            }

            if (_parametersData.debugGizmos.cellingCheck)
            {
                DebugDrawCellingCheck();
            }
        }

        private void DebugDrawGroundCheck()
        {
            Vector3 position = _origin.position;
            bool isGrounded = IsGrounded();

            //Draw ray//
            if (isGrounded)
            {
                Gizmos.color = _parametersData.debugGizmos.groundCheckColorHit;
                Gizmos.DrawLine(position, _groundHit.point);
            }
            else
            {
                Gizmos.color = _parametersData.debugGizmos.groundCheckColor;
                Gizmos.DrawLine(position, position + Vector3.down * _parametersData.groundCheckDistance);
            }

            //Draw Sphere//
            Vector3 center;
            if (isGrounded)
            {
                Gizmos.color = _parametersData.debugGizmos.groundCheckColorHit;

                center = position + Vector3.down * _groundHit.distance;
                Gizmos.DrawWireSphere(center, _parametersData.groundCheckRadius);
            }
            else
            {
                Gizmos.color = _parametersData.debugGizmos.groundCheckColor;

                center = position + Vector3.down * _parametersData.groundCheckDistance;
                Gizmos.DrawWireSphere(center, _parametersData.groundCheckRadius);
            }
        }

        private void DebugDrawSlopeCheck()
        {
            Vector3 position = _origin.position;
            bool isOnSlope = IsOnSlope();

            Vector3 center;
            if (isOnSlope)
            {
                Gizmos.color = _parametersData.debugGizmos.slopeCheckColorHit;
                center = position + Vector3.down * _slopeHitSphere.distance;
                Gizmos.DrawWireSphere(center, _parametersData.slopeCheckSphereRadius);

                //Line//
                Vector3 origin = _slopeHitSphere.point + _slopeCheckRayOffset;
                Gizmos.DrawLine(origin, _slopeHitRay.point);
            }
            else
            {
                Gizmos.color = _parametersData.debugGizmos.slopeCheckColor;
                center = position + Vector3.down * _slopeHitSphere.distance;
                Gizmos.DrawWireSphere(center, _parametersData.slopeCheckSphereRadius);
            }
        }

        private void DebugDrawCellingCheck()
        {
            Vector3 position = _origin.position;
            bool isGrounded = HitCelling();

            //Draw ray//
            if (isGrounded)
            {
                Gizmos.color = _parametersData.debugGizmos.cellingCheckColorHit;
                Gizmos.DrawLine(position, _groundHit.point);
            }
            else
            {
                Gizmos.color = _parametersData.debugGizmos.cellingCheckColor;
                Gizmos.DrawLine(position, position + Vector3.up * _cellingCheckDistance);
            }

            //Draw Sphere//
            Vector3 center;
            if (isGrounded)
            {
                Gizmos.color = _parametersData.debugGizmos.cellingCheckColorHit;

                center = position + Vector3.up * _cellingHit.distance;
                Gizmos.DrawWireSphere(center, _parametersData.cellingCheckRadius);
            }
            else
            {
                Gizmos.color = _parametersData.debugGizmos.cellingCheckColor;

                center = position + Vector3.up * _cellingCheckDistance;
                Gizmos.DrawWireSphere(center, _parametersData.cellingCheckRadius);
            }
        }

#endif

        #endregion Debug Gizmo
    }
}