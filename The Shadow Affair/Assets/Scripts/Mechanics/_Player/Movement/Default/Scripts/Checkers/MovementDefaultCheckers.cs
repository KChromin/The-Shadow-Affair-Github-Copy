using UnityEngine;

namespace SmugRag.Mechanics.Player.Movement.Default
{
    public class MovementDefaultCheckers
    {
        #region Parameters & Setup

        private Transform _origin;

        private MovementDefaultCheckersParameters _parameters;

        private RaycastHit _groundHit;

        public void Setup(MovementDefaultScriptableObject movementParameters, Transform origin)
        {
            _parameters = movementParameters.checkerParameters;
            _origin = origin;
        }

        #endregion Parameters & Setup

        #region Ground Check

        public bool CheckIsGrounded()
        {
            return Physics.SphereCast(_origin.position, _parameters.groundCheckRadius, Vector3.down, out _groundHit, _parameters.groundCheckDistance, _parameters.groundLayers, QueryTriggerInteraction.Ignore);
        }

        public RaycastHit GetGroundHit()
        {
            return _groundHit;
        }

        #endregion Ground Check

        #region Debug Gizmo

#if UNITY_EDITOR

        public void DebugDrawGizmos()
        {
            if (_parameters.debugGizmos.groundCheck)
            {
                DebugDrawGroundCheck();
            }
        }

        private void DebugDrawGroundCheck()
        {
            Vector3 position = _origin.position;
            bool isGrounded = CheckIsGrounded();

            //Draw ray//
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckRayColorHit;
                Gizmos.DrawLine(position, _groundHit.point);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckRayColor;
                Gizmos.DrawLine(position, position + Vector3.down * _parameters.groundCheckDistance);
            }

            //Draw Sphere//
            Vector3 center;
            if (isGrounded)
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckSphereColorHit;

                center = position + Vector3.down * _groundHit.distance;
                Gizmos.DrawWireSphere(center, _parameters.groundCheckRadius);
            }
            else
            {
                Gizmos.color = _parameters.debugGizmos.groundCheckSphereColor;

                center = position + Vector3.down * _parameters.groundCheckDistance;
                Gizmos.DrawWireSphere(center, _parameters.groundCheckRadius);
            }
        }

#endif

        #endregion Debug Gizmo
    }
}