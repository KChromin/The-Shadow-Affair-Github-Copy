using SmugRagGames.Managers.Settings;
using UnityEngine;

namespace SmugRagGames.Player.Rotation
{
    public class PlayerRotationDefault : PlayerMechanicsBase
    {
        #region Variables

        private GameControls _input;

        private SettingsControlScriptableObject _settings;

        #region Transforms

        [Header("Transforms")]
        [SerializeField]
        private Transform playerOrigin;

        [SerializeField]
        private Transform orientation;

        [SerializeField]
        private Transform headPivot;

        #endregion Transforms

        #region Parameters

        [Header("Parameters")]
        [SerializeField]
        private PlayerRotationDefaultScriptableObject parameters;

        #endregion Parameters

        private PlayerRotationPossibilities _possibilities;
        private PlayerRotationCases _cases;

        private float _verticalRotationValue;

        private Vector2 _smoothedInput;
        private Vector2 _smoothedInputCalculation;
        
        private const float MinimalRotationThreshold = 0.2f;

        #endregion Variables

        #region Setup
        
        public void Setup(GameControls input, SettingsControlScriptableObject settings, PlayerRotationPossibilities possibilities, PlayerRotationCases cases)
        {
            _input = input;
            _settings = settings;
            _possibilities = possibilities;
            _cases = cases;
        }

        #endregion Setup

        private void Update()
        {
            if (!IsEnabled) return;
            if (!_possibilities.canRotate) return;

            SmoothInput();
            VerticalRotation();
            HorizontalRotation();
        }

        #region Input Processing
        
        private void SmoothInput()
        {
            _smoothedInput = Vector2.SmoothDamp(_smoothedInput, CurrentInput(), ref _smoothedInputCalculation, parameters.smoothingSpeedValue);

            #region Case

            _cases.isRotating = _smoothedInput.magnitude > MinimalRotationThreshold;

            #endregion Case
        }

        private Vector2 CurrentInput()
        {
            Vector2 inputRaw = _input.Player.Look.ReadValue<Vector2>();

            return InputWithAppliedSettings(inputRaw);
        }

        private Vector2 InputWithAppliedSettings(Vector2 rawInput)
        {
            //Invert Y axis//
            if (_settings.lookInvertYAxis)
            {
                rawInput.y = -rawInput.y;
            }

            //Separate sensitivities//
            if (_settings.lookSeparateSensitivityAxes)
            {
                Vector2 sensitivityValues = new(_settings.lookSensitivityXAxis, _settings.lookSensitivityYAxis);

                return new Vector2(rawInput.x * sensitivityValues.x, rawInput.y * sensitivityValues.y);
            }

            //General sensitivity//
            float sensitivityValue = _settings.lookSensitivityGeneral;
            return rawInput * sensitivityValue;
        }

        #endregion Input Processing

        #region Execute Rotations

        private void VerticalRotation()
        {
            //Check if camera can move//
            if (!_possibilities.canRotateY) return;

            //Modify current delta//
            _verticalRotationValue -= _smoothedInput.y;

            //Clamp between angles//
            _verticalRotationValue = Mathf.Clamp(_verticalRotationValue, -parameters.maximalAngleDown, parameters.maximalAngleUp);

            //Apply rotation to head pivot//
            headPivot.localRotation = Quaternion.Euler(playerOrigin.right * _verticalRotationValue);
        }

        private void HorizontalRotation()
        {
            //Check if player can rotate//
            if (!_possibilities.canRotateX) return;

            //Rotate Orientation//
            orientation.Rotate(_smoothedInput.x * playerOrigin.up);
        }

        #endregion Execute Rotations
        
    }
}