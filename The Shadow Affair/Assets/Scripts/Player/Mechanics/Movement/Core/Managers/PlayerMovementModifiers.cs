using System;
using System.Collections.Generic;
using UnityEngine;

namespace SmugRagGames.Player.Movement
{
    //Modifier Change Requests can only be made by OnTrigger, or OnCollision//
    public class PlayerMovementModifiers
    {
        #region Modifiers Data

        [Header("Current Modifiers")]
        public ModifiersDataClass CurrentModifiers = new ModifiersDataClass();

        [Serializable]
        public class ModifiersDataClass
        {
            [Header("General")]
            [Range(0.01f, 10f)]
            public float moveSpeedMultiplier = 1;
        }

        #endregion Modifiers Data

        #region Modifier Change Request

        public class ModifierChangeRequestClass
        {
            public PlayerMovementModifierValueName ModifierName;
            public float ModifierValueAddend; //Addition of full value//
            public float ModifierValueFactor; //Multiplier//
        }

        private List<ModifierChangeRequestClass> _modifierChangeRequests = new List<ModifierChangeRequestClass>();

        #endregion Modifier Change Request

        #region Public methodes

        public void RequestModifierChange(ModifierChangeRequestClass newRequest)
        {
            _modifierChangeRequests.Add(newRequest);
        }

        public void UpdateModifierValues()
        {
            ResetModifiersValues();
            SumAllModifierChangeRequests();
            ClearModifierList();
        }

        #endregion Public methodes

        #region Private methodes

        private void ResetModifiersValues()
        {
            CurrentModifiers.moveSpeedMultiplier = 1;
        }

        private void SumAllModifierChangeRequests()
        {
            foreach (ModifierChangeRequestClass request in _modifierChangeRequests)
            {
                switch (request.ModifierName)
                {
                    case PlayerMovementModifierValueName.MoveSpeed:
                        CurrentModifiers.moveSpeedMultiplier += request.ModifierValueAddend;
                        CurrentModifiers.moveSpeedMultiplier *= request.ModifierValueFactor;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ClearModifierList()
        {
            _modifierChangeRequests.Clear();
        }

        #endregion Private methodes
    }

    public enum PlayerMovementModifierValueName
    {
        MoveSpeed
    }
}