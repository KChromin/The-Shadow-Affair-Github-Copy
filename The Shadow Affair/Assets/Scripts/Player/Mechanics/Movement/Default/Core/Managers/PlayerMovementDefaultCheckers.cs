using UnityEngine;

namespace SmugRagGames.Player
{
    public class PlayerMovementDefaultCheckers
    {
        public PlayerMovementDefaultCheckers(Transform checkersOrigin, PlayerMovementDefaultCheckersScriptableObject parameters, float startingPlayerHeight)
        {
            _origin = checkersOrigin;
            _parameters = parameters;
            _startingPlayerHeight = startingPlayerHeight;

            float halfHeight = _startingPlayerHeight / 2;
            _cellingCheckDistance = (halfHeight - _parameters.cellingCheckRadius) + _parameters.cellingCheckErrorMargin;

            _slopeCheckRayOffset = new Vector3(0, _parameters.slopeCheckRayOffsetY, 0);
        }

        private readonly Transform _origin;
        private readonly PlayerMovementDefaultCheckersScriptableObject _parameters;

        private float _startingPlayerHeight;
        private readonly float _cellingCheckDistance;

        //RayCasts//
        private RaycastHit _groundHit;
        private RaycastHit _slopeHitSphere;
        private RaycastHit _slopeHitRay;
        private RaycastHit _cellingHit;

        private Vector3 _slopeCheckRayOffset;
        private Vector3 _slopeCheckNormal;

        private bool _slopeIsTooSteep;

        #region Check Methodes

        //Grounding//
        public bool IsGrounded()
        {
            return Physics.SphereCast(_origin.position, _parameters.groundCheckRadius, Vector3.down, out _groundHit, _parameters.groundCheckDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore);
        }

        //Slope//
        public bool IsOnSlope()
        {
            //Sphere Check//
            if (Physics.SphereCast(_origin.position, _parameters.slopeCheckSphereRadius, Vector3.down, out _slopeHitSphere, _parameters.slopeCheckSphereDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore))
            {
                //Check Ray//
                if (Physics.Raycast(_slopeHitSphere.point + _slopeCheckRayOffset, Vector3.down, out _slopeHitRay, _parameters.slopeCheckRayDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore))
                {
                    _slopeCheckNormal = _slopeHitRay.normal;
                }
                else
                {
                    //Check Ray but from grounding//
                    if (Physics.Raycast(_groundHit.point + _slopeCheckRayOffset, Vector3.down, out _slopeHitRay, _parameters.slopeCheckRayDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore))
                    {
                        _slopeCheckNormal = _slopeHitRay.normal;
                    }
                }
            }
            else
            {
                //Check Ray but from grounding//
                if (Physics.Raycast(_groundHit.point + _slopeCheckRayOffset, Vector3.down, out _slopeHitRay, _parameters.slopeCheckRayDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore))
                {
                    _slopeCheckNormal = _slopeHitRay.normal;
                }
            }

            return IsSlopeAngleInMinimumRange(_slopeCheckNormal);
        }

        private bool IsSlopeAngleInMinimumRange(Vector3 slopeHitNormal)
        {
            float hitAngle = Vector3.Angle(slopeHitNormal, Vector3.up);

            if (_parameters.slopeCheckAngleMinimum < hitAngle)
            {
                if (_parameters.slopeCheckAngleMaximum < hitAngle)
                {
                    _slopeIsTooSteep = true;
                }
                else
                {
                    _slopeIsTooSteep = false;
                }

                return true;
            }
            else
            {
                _slopeIsTooSteep = false;
            }

            return false;
        }

        //Celling//

        public bool IsHittingCelling()
        {
            return Physics.SphereCast(_origin.position, _parameters.cellingCheckRadius, Vector3.up, out _cellingHit, _cellingCheckDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore);
        }

        #endregion Check Methodes

        #region Get methodes

        //Grounding//
        public RaycastHit GetGroundHit()
        {
            return _groundHit;
        }

        //Slope//
        public bool GetIsSlopeTooSteep()
        {
            return _slopeIsTooSteep;
        }

        public Vector3 GetSlopeNormal()
        {
            return _slopeCheckNormal;
        }

        //Celling//
        public RaycastHit GetCellingHit()
        {
            return _cellingHit;
        }

        #endregion Get methodes

        #region Debug Gizmo

#if UNITY_EDITOR
        public void DrawGizmos()
        {
            if (_parameters.debugGizmos.groundCheckDraw)
            {
                DrawGroundCheck();
            }

            if (_parameters.debugGizmos.slopeCheckDraw)
            {
                DrawSlopeCheck();
            }

            if (_parameters.debugGizmos.cellingCheckDraw)
            {
                DrawCellingCheck();
            }
        }

        private void DrawGroundCheck()
        {
            Vector3 position = _origin.position;
            bool isGrounded = IsGrounded();

            //Draw ray//
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckColorTrue;
                Gizmos.DrawLine(position, _groundHit.point);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckColorFalse;
                Gizmos.DrawLine(position, position + Vector3.down * _parameters.groundCheckDistance);
            }

            //Draw Sphere//
            Vector3 center;
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckColorTrue;

                center = position + Vector3.down * _groundHit.distance;
                Gizmos.DrawWireSphere(center, _parameters.groundCheckRadius);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckColorFalse;

                center = position + Vector3.down * _parameters.groundCheckDistance;
                Gizmos.DrawWireSphere(center, _parameters.groundCheckRadius);
            }
        }

        private void DrawSlopeCheck()
        {
            Vector3 position = _origin.position;
            bool isOnSlope = IsOnSlope();

            Vector3 center;
            if (isOnSlope)
            {
                Gizmos.color = _parameters.debugGizmos.slopeCheckColorTrue;
                center = position + Vector3.down * _slopeHitSphere.distance;
                Gizmos.DrawWireSphere(center, _parameters.slopeCheckSphereRadius);

                //Line//
                Vector3 origin = _slopeHitSphere.point + _slopeCheckRayOffset;
                Gizmos.DrawLine(origin, _slopeHitRay.point);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.slopeCheckColorFalse;
                center = position + Vector3.down * _slopeHitSphere.distance;
                Gizmos.DrawWireSphere(center, _parameters.slopeCheckSphereRadius);
            }
        }

        private void DrawCellingCheck()
        {
            Vector3 position = _origin.position;
            bool isGrounded = IsHittingCelling();

            //Draw ray//
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.cellingCheckColorTrue;
                Gizmos.DrawLine(position, _groundHit.point);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.cellingCheckColorFalse;
                Gizmos.DrawLine(position, position + Vector3.up * _cellingCheckDistance);
            }

            //Draw Sphere//
            Vector3 center;
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.cellingCheckColorTrue;

                center = position + Vector3.up * _cellingHit.distance;
                Gizmos.DrawWireSphere(center, _parameters.cellingCheckRadius);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.cellingCheckColorFalse;

                center = position + Vector3.up * _cellingCheckDistance;
                Gizmos.DrawWireSphere(center, _parameters.cellingCheckRadius);
            }
        }
#endif

        #endregion Debug Gizmo
    }
}