using SmugRag.Managers;
using SmugRag.Managers.Settings;
using SmugRag.ScriptableObjects.Settings;
using UnityEngine;

namespace SmugRag.Player.Mechanics.Rotation
{
    public class RotationDefault : MonoBehaviour
    {
        #region Variables

        private GameControls _input;
        private SettingsControlsScriptableObject _controlSettings;

        #region Parameters

        [Header("Look Parameters")]
        [SerializeField, Range(0, 90)]
        private int lookMaximalAngleUp = 80;
        [SerializeField, Range(0, 90)]
        private int lookMaximalAngleDown = 80;

        [Header("Look Smoothing")]
        [SerializeField, Range(0, 0.05f)]
        private float lookSmoothingSpeed = 0.02f;

        #endregion Parameters

        #region Transforms

        [Header("Transforms")]
        [SerializeField]
        private Transform rootTransform;
        [SerializeField]
        private Transform headPivot;
        [SerializeField]
        private Transform orientation;

        #endregion Transforms

        private float _verticalRotationValue;
        private Vector2 _smoothedInput;
        private Vector2 _smoothedInputCalculation;

        #endregion Variables

        #region Setups

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            //Set Input Controls//
            _input = InputManager.Instance.CurrentInput;

            //Current Control Settings//
            SettingsManagerData currentSettings = SettingsManager.Instance.CurrentSettings;
            _controlSettings = currentSettings.Controls;
        }

        #endregion Setups

        private void Update()
        {
            UpdateSmoothedInput();
            VerticalRotation();
            HorizontalRotation();
        }

        #region Input Processing

        private void UpdateSmoothedInput()
        {
            _smoothedInput = Vector2.SmoothDamp(_smoothedInput, GetCurrentInput(), ref _smoothedInputCalculation, lookSmoothingSpeed);
        }

        private Vector2 GetCurrentInput()
        {
            Vector2 inputRaw = _input.Default.Look.ReadValue<Vector2>();

            return GetInputWithAppliedSettings(inputRaw);
        }

        private Vector2 GetInputWithAppliedSettings(Vector2 rawInput)
        {
            //Invert Y axis//
            if (_controlSettings.lookInvertYAxis)
            {
                rawInput.y = -rawInput.y;
            }

            //Separate Sensitivities//
            if (_controlSettings.lookUseSeparateSensitivityAxes)
            {
                //Get values from current settings//
                Vector2 sensitivityValues = new (_controlSettings.lookSensitivitySeparateAxesX, _controlSettings.lookSensitivitySeparateAxesY);
                
                return new Vector2(rawInput.x * sensitivityValues.x, rawInput.y * sensitivityValues.y);
            }

            //One sensitivity//
            float sensitivityValue = _controlSettings.lookSensitivityGeneral;
            return rawInput * sensitivityValue;
        }

        #endregion Input Processing

        #region Execute Rotations

        private void VerticalRotation()
        {
            //Add current delta//
            _verticalRotationValue -= _smoothedInput.y;

            //Clamp between max angles//
            _verticalRotationValue = Mathf.Clamp(_verticalRotationValue, -lookMaximalAngleDown, lookMaximalAngleUp);

            //Update head pivot rotation//
            headPivot.localRotation = Quaternion.Euler(rootTransform.right * _verticalRotationValue);
        }

        private void HorizontalRotation()
        {
            orientation.Rotate(_smoothedInput.x * rootTransform.up);
        }

        #endregion Execute Rotations
    }
}