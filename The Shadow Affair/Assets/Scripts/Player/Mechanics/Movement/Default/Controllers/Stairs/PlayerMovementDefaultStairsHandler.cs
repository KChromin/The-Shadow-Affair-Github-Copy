using System;
using SmugRagGames.Player.Movement.Input;
using UnityEngine;
using UnityEngine.UI;

namespace SmugRagGames.Player.Movement.Default
{
    public class PlayerMovementDefaultStairsHandler
    {
        public PlayerMovementDefaultStairsHandler(Transform orientation, PlayerMovementInput input, PlayerMovementDefaultScriptableObject parameters, PlayerMovementDefaultCheckersScriptableObject checkersParameters, PlayerMovementDefaultCheckers checkers, PlayerMovementDefaultExecution execution)
        {
            _orientation = orientation;
            _parameters = parameters;
            _checkersParameters = checkersParameters;
            _checkers = checkers;
            _input = input;
            _execution = execution;

            //Up Check Offset//
            _stepCheckHighOriginOffset = new Vector3(0, _parameters.stairsHandlerParameters.stepHeight, 0);
            _stepCheckLowOriginOffset = new Vector3(0, _parameters.stairsHandlerParameters.stepHeightMinimal, 0);
        }

        private readonly Transform _orientation;
        private readonly PlayerMovementDefaultScriptableObject _parameters;
        private readonly PlayerMovementDefaultCheckersScriptableObject _checkersParameters;
        private readonly PlayerMovementDefaultCheckers _checkers;
        private readonly PlayerMovementInput _input;
        private readonly PlayerMovementDefaultExecution _execution;

        private readonly Vector3 _stepCheckHighOriginOffset;
        private readonly Vector3 _stepCheckLowOriginOffset;

        public enum StepUpMode
        {
            Walk,
            Run,
            CrouchWalk
        }

        #region Private methodes

        private Vector3 GetCurrentDirection()
        {
            //Get Input//
            Vector2 input = _input.GetValueMove();

            //Convert Input to 3D//
            return _orientation.localRotation * new Vector3(input.x, 0, input.y);
        }

        private Vector3 GetCurrentDirectionAfterProjection(Vector3 currentDirection)
        {
            return Vector3.ProjectOnPlane(currentDirection, _checkers.GetSlopeNormal());
        }

        private bool CheckForSteps(Vector3 orientationPosition, Vector3 currentDirection)
        {
            #region Debug Gizmos

#if UNITY_EDITOR
            if (_parameters.stairsHandlerParameters.stepCheckDrawGizmos)
            {
                Debug.DrawLine(orientationPosition + _stepCheckLowOriginOffset, orientationPosition + _stepCheckLowOriginOffset + (currentDirection * _parameters.stairsHandlerParameters.stepCheckDistanceLow), Color.yellow, 0.1f);
                Debug.DrawLine(orientationPosition + _stepCheckHighOriginOffset, orientationPosition + _stepCheckHighOriginOffset + (currentDirection * _parameters.stairsHandlerParameters.stepCheckDistanceHigh), Color.blue, 0.1f);
            }
#endif

            #endregion Debug Gizmos

            //Check Low//
            if (!Physics.Raycast(orientationPosition + _stepCheckLowOriginOffset, currentDirection, _parameters.stairsHandlerParameters.stepCheckDistanceLow, _checkersParameters.groundLayers, QueryTriggerInteraction.Ignore)) return false;

            //Check High//
            return !Physics.Raycast(orientationPosition + _stepCheckHighOriginOffset, currentDirection, _parameters.stairsHandlerParameters.stepCheckDistanceHigh, _checkersParameters.groundLayers, QueryTriggerInteraction.Ignore);
        }

        private Vector3 UpStepVector(StepUpMode stepUpMode)
        {
            float stepUpForce = stepUpMode switch
            {
                StepUpMode.Walk => _parameters.stairsHandlerParameters.stepUpForceWalk,
                StepUpMode.Run => _parameters.stairsHandlerParameters.stepUpForceRun,
                StepUpMode.CrouchWalk => _parameters.stairsHandlerParameters.stepUpForceCrouchWalk,
                _ => throw new ArgumentOutOfRangeException(nameof(stepUpMode), stepUpMode, null)
            };

            return Vector3.up * (_parameters.physicsParameters.AntiDragValueForDrag10 * stepUpForce);
        }

        #endregion Private methodes

        #region Public methodes

        public void StepClimb(StepUpMode stepUpMode)
        {
            Vector3 orientationPosition = _orientation.position;

            //Get Current Direction//
            Vector3 currentDirection = GetCurrentDirection();

            //Check Parameters//
            int iterationCount = (_parameters.stairsHandlerParameters.stepCheckIterationPerSide * 2) + 1;
            float fartherNegativeCheckValue = -(_parameters.stairsHandlerParameters.stepCheckIterationPerSide * _parameters.stairsHandlerParameters.stepCheckIterationOffsetInDegrees);

            for (int i = 0; i < iterationCount; i++)
            {
                //Current movement direction + offsets, for step check//
                //Direction Offset//
                float checkDirectionOffset = fartherNegativeCheckValue + (i * _parameters.stairsHandlerParameters.stepCheckIterationOffsetInDegrees);

                //New directions for projection//
                Vector3 currentDirectionWithOffset = Quaternion.Euler(0, checkDirectionOffset, 0) * currentDirection;
                Vector3 currentDirectionWithOffsetAfterProjection = GetCurrentDirectionAfterProjection(currentDirectionWithOffset);

                //On slope when going down//
                Vector3 currentDirectionToCheck = currentDirectionWithOffsetAfterProjection.y > 0 ? currentDirectionWithOffsetAfterProjection : currentDirectionWithOffset;

                //Rotate Current Direction For Checks//
                if (!CheckForSteps(orientationPosition, currentDirectionToCheck)) continue;

                //Execute//
                _execution.StepClimbing(UpStepVector(stepUpMode));

                break;
            }
        }

        #endregion Public methodes
    }
}